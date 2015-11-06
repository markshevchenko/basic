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

        private readonly IProgramRepository programRepository;

        /// <inheritdoc />
        public virtual bool IsClosed { get; private set; }

        /// <inheritdoc />
        public virtual bool IsRunning { get { return Runner != null; } }

        /// <inheritdoc />
        public virtual string LastUsedName { get; private set; }

        /// <inheritdoc />
        public virtual IInputOutput InputOutput { get; private set; }

        /// <inheritdoc />
        public virtual IDictionary<string, dynamic> Variables { get; private set; }

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

            this.programRepository = programRepository;

            InputOutput = inputOutput;
            InputOutput.OnBreak += InputOutput_OnBreak;
            LastUsedName = null;
            IsClosed = false;
            IsDisposed = false;
            Variables = new Dictionary<string, dynamic>();
            Lines = new SortedList<int, IStatement>();
            Variables[RandomKey] = new Random();
        }

        /// <inheritdoc />
        public virtual void Close()
        {
            IsClosed = true;
        }

        /// <inheritdoc />
        public virtual void Save(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            programRepository.Save(name, Lines);
            LastUsedName = name;
        }

        /// <inheritdoc />
        public virtual void Load(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            var lines = programRepository.Load(name);
            Lines = new SortedList<int, IStatement>(lines);
            LastUsedName = name;
        }

        /// <inheritdoc />
        public virtual ProgramResult Run()
        {
            if (Runner != null)
                throw new InvalidOperationException(ErrorMessages.ProgramIsRunning);

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
            if (Runner == null)
                throw new InvalidOperationException(ErrorMessages.ProgramIsNotRunning);

            Runner.Complete();
        }

        /// <inheritdoc />
        public virtual void Goto(int number)
        {
            if (Runner == null)
                throw new InvalidOperationException(ErrorMessages.ProgramIsNotRunning);

            Runner.Goto(number);
        }

        /// <inheritdoc />
        public virtual void Randomize(int seed)
        {
            Variables[RandomKey] = new Random(seed);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (IsDisposed)
                return;

            Dispose(true);

            IsDisposed = true;
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
                InputOutput.OnBreak -= InputOutput_OnBreak;
        }

        private void InputOutput_OnBreak(object sender, EventArgs e)
        {
            if (Runner == null)
                return;

            Runner.Break();
        }
    }
}
