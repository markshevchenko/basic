namespace LearningInterpreter.RunTime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using LearningInterpreter.IO;

    /// <summary>
    /// Implements the run-time environment.
    /// </summary>
    public class RunTimeEnvironment : IRunTimeEnvironment, IDisposable
    {
        private readonly IInputOutput inputOutput;
        private readonly IProgramRepository programRepository;
        private readonly Variables variables;

        /// <inheritdoc />
        public virtual bool IsClosed { get { return IsDisposed; } }

        /// <inheritdoc />
        public virtual bool IsRunning { get { return Runner != null; } }

        /// <inheritdoc />
        public virtual IInputOutput InputOutput { get { return inputOutput; } }

        /// <summary>
        /// Gets the dictionary of the existing variables.
        /// </summary>
        public virtual Variables Variables { get { return variables; } }

        /// <summary>
        /// Gets the list of the program lines.
        /// </summary>
        public virtual List<IAstNode> Lines { get; private set; }

        /// <inheritdoc />
        IReadOnlyList<IAstNode> IRunTimeEnvironment.Lines { get { return Lines; } }

        /// <inheritdoc />
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// The runner of program.
        /// </summary>
        public ProgramRunner Runner { get; private set; }

        /// <summary>
        /// The stack of multiline loops.
        /// </summary>
        internal Stack<MultilineLoop> StackOfLoops { get; private set; }

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
            this.variables = new Variables();

            IsDisposed = false;
            Lines = new List<IAstNode>();
            StackOfLoops = new Stack<MultilineLoop>();
        }

        /// <inheritdoc />
        public int BinarySearch(IAstNode line)
        {
            if (line == null)
                throw new ArgumentNullException("line");

            return Lines.BinarySearch(line);
        }

        /// <inheritdoc />
        public void AddOrUpdate(IAstNode line)
        {
            if (line == null)
                throw new ArgumentNullException("line");

            var index = Lines.BinarySearch(line);

            if (index < 0)
                Lines.Insert(~index, line);
            else
                Lines[index] = line;
        }


        /// <inheritdoc />
        public void RemoveRange(int index, int count)
        {
            Lines.RemoveRange(index, count);
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
            variables.LastUsedProgramName = name;
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
            variables.LastUsedProgramName = name;
        }

        /// <inheritdoc />
        public virtual ProgramResult Run()
        {
            ThrowIfDisposed();
            ThrowIfRunning();

            StartRun();
            try
            {
                return TakeRunSteps();
            }
            finally
            {
                StopRun();
            }
        }

        protected virtual internal void StartRun()
        {
            Runner = new ProgramRunner(Lines);
        }

        protected virtual internal ProgramResult TakeRunSteps()
        {
            while (Runner.MoveNext())
                Runner.ExecuteCurrentStatement(this);

            if (Runner.IsBroke)
                return ProgramResult.Broken;

            return ProgramResult.Completed;
        }

        protected virtual internal void StopRun()
        {
            Runner = null;
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

            Variables.Random = new Random(seed);
        }

        /// <inheritdoc />
        public bool IsLoopStarted
        {
            get
            {
                ThrowIfDisposed();
                ThrowIfNotRunning();

                return StackOfLoops.Any(l => l.StartLine == Runner.RunningLine);
            }
        }

        /// <inheritdoc />
        public void StartLoop(ILoop loop)
        {
            ThrowIfDisposed();
            ThrowIfNotRunning();

            if (loop == null)
                throw new ArgumentNullException("loop");

            var multilineLoop = new MultilineLoop(Runner.RunningLine, loop);
            StackOfLoops.Push(multilineLoop);
        }

        /// <inheritdoc />
        public bool IsLastLoopOver
        {
            get
            {
                ThrowIfDisposed();
                ThrowIfNotRunning();
                ThrowIfThereIsNotLastLoop();

                return StackOfLoops.Peek().IsOver;
            }
        }

        /// <inheritdoc />
        public void TakeLastLoopStep()
        {
            ThrowIfDisposed();
            ThrowIfNotRunning();
            ThrowIfThereIsNotLastLoop();

            StackOfLoops.Peek().TakeStep();
        }

        /// <inheritdoc />
        public void StopLastLoop()
        {
            ThrowIfDisposed();
            ThrowIfNotRunning();
            ThrowIfThereIsNotLastLoop();

            StackOfLoops.Pop();
        }

        /// <inheritdoc />
        public void RepeatLastLoop()
        {
            ThrowIfDisposed();
            ThrowIfNotRunning();
            ThrowIfThereIsNotLastLoop();

            var startLabel = GetStartLabelOfLastLoop();
            Goto(startLabel);
        }

        internal string GetStartLabelOfLastLoop()
        {
            return StackOfLoops.Peek().StartLine.Label;
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

        internal void InputOutput_OnBreak(object sender, EventArgs e)
        {
            if (!IsRunning)
                return;

            Runner.Break();
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
            if (StackOfLoops.Count == 0)
                throw new InvalidOperationException(ErrorMessages.UnexpectedEndOfLoop);
        }
    }
}
