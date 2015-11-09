namespace LearningBasic.RunTime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using LearningBasic.IO;

    /// <summary>
    /// Implements the run-time environment.
    /// </summary>
    public class RunTimeEnvironment : IRunTimeEnvironment, IDisposable
    {
        public const string RandomKey = "@Random";

        private readonly Stack<MultilineLoop> stackOfLoops;
        private readonly IInputOutput inputOutput;
        private readonly IProgramRepository programRepository;
        private readonly Dictionary<string, dynamic> variables;

        /// <inheritdoc />
        public virtual bool IsClosed { get { return IsDisposed; } }

        /// <inheritdoc />
        public virtual bool IsRunning { get { return Runner != null; } }

        /// <inheritdoc />
        public virtual string LastUsedName { get; private set; }

        /// <inheritdoc />
        public virtual IInputOutput InputOutput { get { return inputOutput; } }

        /// <summary>
        /// Gets the dictionary of the existing variables.
        /// </summary>
        public virtual Dictionary<string, dynamic> Variables { get { return variables; } }

        /// <inheritdoc />
        IDictionary<string, dynamic> IRunTimeEnvironment.Variables { get { return Variables; } }

        /// <summary>
        /// Gets the list of the program lines.
        /// </summary>
        public virtual List<ILine> Lines { get; private set; }

        /// <inheritdoc />
        IReadOnlyList<ILine> IRunTimeEnvironment.Lines { get { return Lines; } }

        /// <inheritdoc />
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// The runner of program.
        /// </summary>
        public ProgramRunner Runner { get; private set; }

        /// <summary>
        /// Creates an instance of <see cref="RunTimeEnvironment"/>.
        /// </summary>
        /// <param name="inputOutput">The input/output object.</param>
        /// <param name="programRepository">The program repository.</param>
        public RunTimeEnvironment(IInputOutput inputOutput, IProgramRepository programRepository)
        {
            if (inputOutput == null)
                throw new ArgumentNullException("inputOutput");

            if (programRepository == null)
                throw new ArgumentNullException("programRepository");

            this.inputOutput = inputOutput;
            this.inputOutput.OnBreak += InputOutput_OnBreak;
            this.programRepository = programRepository;
            this.variables = new Dictionary<string, dynamic>();
            this.variables[RandomKey] = new Random();
            this.stackOfLoops = new Stack<MultilineLoop>();

            LastUsedName = null;
            IsDisposed = false;
            Lines = new List<ILine>();
        }

        /// <inheritdoc />
        public void AddOrUpdate(ILine line)
        {
            var index = Lines.BinarySearch(line);

            if (index < 0)
                Lines.Insert(~index, line);
            else
                Lines[index] = line;
        }

        /// <inheritdoc />
        public int Remove(ILine from, ILine to)
        {
            var fromIndex = Lines.BinarySearch(from);
            if (fromIndex < 0)
                fromIndex = ~fromIndex;

            var toIndex = Lines.BinarySearch(to);
            if (toIndex < 0)
                toIndex = ~toIndex;

            var count = toIndex - fromIndex + 1;
            Lines.RemoveRange(fromIndex, count);

            return count;
        }

        /// <inheritdoc />
        public virtual void Close()
        {
            ThrowIfDisposed();

            Dispose();
        }

        /// <inheritdoc />
        public virtual void Save(string name)
        {
            ThrowIfDisposed();
            ThrowIfRunning();

            if (name == null)
                throw new ArgumentNullException("name");

            programRepository.Save(name, Lines);
            LastUsedName = name;
        }

        /// <inheritdoc />
        public virtual void Load(string name)
        {
            ThrowIfDisposed();
            ThrowIfRunning();

            if (name == null)
                throw new ArgumentNullException("name");

            var lines = programRepository.Load(name);
            Lines = new List<ILine>(lines);
            LastUsedName = name;
        }

        /// <inheritdoc />
        public virtual ProgramResult Run()
        {
            ThrowIfDisposed();
            ThrowIfRunning();

            Runner = new ProgramRunner(Lines);
            try
            {
                return Run(Runner);
            }
            finally
            {
                Runner = null;
            }
        }

        private ProgramResult Run(ProgramRunner programRunner)
        {
            while (programRunner.MoveNext())
            {
                programRunner.RunningLine.Statement.Execute(this);
            }

            if (programRunner.IsBroke)
                return ProgramResult.Broken;

            return ProgramResult.Completed;
        }

        /// <inheritdoc />
        public virtual void End()
        {
            ThrowIfDisposed();
            ThrowIfNotRunning();

            Runner.Complete();
        }

        /// <inheritdoc />
        public virtual void Goto(string label)
        {
            ThrowIfDisposed();
            ThrowIfNotRunning();

            Runner.Goto(label);
        }

        /// <inheritdoc />
        public virtual void Randomize(int seed)
        {
            ThrowIfDisposed();

            Variables[RandomKey] = new Random(seed);
        }

        /// <inheritdoc />
        public void StartLoop(ILoop loop)
        {
            ThrowIfDisposed();
            ThrowIfNotRunning();

            var loopStartLine = Runner.RunningLine;
            var isLoopAlreadyStarted = stackOfLoops.Any(l => l.StartLine == loopStartLine);

            if (isLoopAlreadyStarted)
                return;

            var multilineLoop = new MultilineLoop(loopStartLine, loop);
            stackOfLoops.Push(multilineLoop);
        }

        /// <inheritdoc />
        public bool IsLastLoopOver
        {
            get
            {
                ThrowIfDisposed();
                ThrowIfNotRunning();
                ThrowIfThereIsNotLastLoop();

                return stackOfLoops.Peek().IsOver;
            }
        }

        /// <inheritdoc />
        public void TakeLastLoopStep()
        {
            ThrowIfDisposed();
            ThrowIfNotRunning();
            ThrowIfThereIsNotLastLoop();

            stackOfLoops.Peek().TakeStep();
        }

        /// <inheritdoc />
        public void StopLastLoop()
        {
            ThrowIfDisposed();
            ThrowIfNotRunning();
            ThrowIfThereIsNotLastLoop();

            stackOfLoops.Pop();
        }

        /// <inheritdoc />
        public void RepeatLastLoop()
        {
            ThrowIfDisposed();
            ThrowIfNotRunning();
            ThrowIfThereIsNotLastLoop();

            Goto(stackOfLoops.Peek().StartLine.Label);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (IsDisposed)
                return;

            if (isDisposing)
                InputOutput.OnBreak -= InputOutput_OnBreak;

            IsDisposed = true;
        }

        private void ThrowIfDisposed()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().FullName);
        }

        private void ThrowIfRunning()
        {
            if (IsRunning)
                throw new InvalidOperationException(ErrorMessages.ProgramIsRunning);
        }

        private void ThrowIfNotRunning()
        {
            if (!IsRunning)
                throw new InvalidOperationException(ErrorMessages.ProgramIsNotRunning);
        }

        private void ThrowIfThereIsNotLastLoop()
        {
            if (stackOfLoops.Count == 0)
                throw new InvalidOperationException(ErrorMessages.UnexpectedEndOfLoop);
        }

        private void InputOutput_OnBreak(object sender, EventArgs e)
        {
            if (!IsRunning)
                return;

            Runner.Break();
        }
    }
}
