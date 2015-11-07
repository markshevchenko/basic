namespace LearningBasic.Evaluating
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Implements the run-time environment.
    /// </summary>
    public class RunTimeEnvironment : IRunTimeEnvironment, IDisposable
    {
        public const string RandomKey = "@Random";

        private readonly Stack<MultilineLoop> multilineLoops;
        private readonly IInputOutput inputOutput;
        private readonly IProgramRepository programRepository;
        private readonly IDictionary<string, dynamic> variables;

        /// <inheritdoc />
        public virtual bool IsClosed { get { return IsDisposed; } }

        /// <inheritdoc />
        public virtual bool IsRunning { get { return Runner != null; } }

        /// <inheritdoc />
        public virtual string LastUsedName { get; private set; }

        /// <inheritdoc />
        public virtual IInputOutput InputOutput { get { return inputOutput; } }

        /// <inheritdoc />
        public virtual IDictionary<string, dynamic> Variables { get { return variables; } }

        /// <inheritdoc />
        public virtual SortedList<int, IStatement> Lines { get; private set; }

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
            this.multilineLoops = new Stack<MultilineLoop>();

            LastUsedName = null;
            IsDisposed = false;
            Lines = new SortedList<int, IStatement>();
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
            Lines = new SortedList<int, IStatement>(lines);
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

        /// <summary>
        /// Evaluates statements of a program as long as they still have.
        /// </summary>
        /// <param name="programRunner">The program runner.</param>
        /// <returns>The result if evaluation.</returns>
        protected virtual ProgramResult Run(ProgramRunner programRunner)
        {
            while (programRunner.MoveNext())
            {
                programRunner.CurrentStatement.Execute(this);
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
        public virtual void Goto(int number)
        {
            ThrowIfDisposed();
            ThrowIfNotRunning();

            Runner.Goto(number);
        }

        /// <inheritdoc />
        public virtual void Randomize(int seed)
        {
            ThrowIfDisposed();

            Variables[RandomKey] = new Random(seed);
        }

        /// <inheritdoc />
        public void StartMultilineLoop(ILoop loop)
        {
            ThrowIfDisposed();
            ThrowIfNotRunning();

            var multilineLoop = new MultilineLoop(Runner.NextLineNumber, loop);
            multilineLoops.Push(multilineLoop);
        }

        /// <inheritdoc />
        public bool TakeLastMultilineLoopStep()
        {
            ThrowIfDisposed();
            ThrowIfNotRunning();

            if (multilineLoops.Count == 0)
                throw new RunTimeException(ErrorMessages.NextWithoutFor);

            var multilineLoop = multilineLoops.Peek();
            multilineLoop.TakeStep();

            if (multilineLoop.IsOver)
            {
                multilineLoops.Pop();
                return false;
            }

            Goto(multilineLoop.StartLineNumber);
            return true;
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

        private void InputOutput_OnBreak(object sender, EventArgs e)
        {
            if (!IsRunning)
                return;

            Runner.Break();
        }
    }
}
