namespace LearningBasic.Evaluating
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Implements the run-time environment.
    /// </summary>
    public class RunTimeEnvironment : IRunTimeEnvironment, IDisposable
    {
        private readonly IProgramRepository programRepository;
        private ProgramRunner programRunner;

        /// <inheritdoc />
        public string LastUsedName { get; private set; }

        /// <inheritdoc />
        public IInputOutput InputOutput { get; private set; }

        /// <inheritdoc />
        public bool IsClosed { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the current environment has been disposed of.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <inheritdoc />
        public IDictionary<string, dynamic> Variables { get; private set; }

        /// <inheritdoc />
        public SortedList<int, IStatement> Lines { get; private set; }

        /// <inheritdoc />
        public bool IsRunning { get { return programRunner != null; } }

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
        }

        /// <inheritdoc />
        public void Close()
        {
            IsClosed = true;
        }

        /// <inheritdoc />
        public void Save()
        {
            if (LastUsedName == null)
            {
                var name = AskUserForName();
                programRepository.Save(name, Lines);
                LastUsedName = name;
            }
            else
                programRepository.Save(LastUsedName, Lines);
        }

        private string AskUserForName()
        {
            InputOutput.Write(Messages.InputProgramName);
            return InputOutput.ReadLine();
        }

        /// <inheritdoc />
        public void Save(string name)
        {
            programRepository.Save(name, Lines);
            LastUsedName = name;
        }

        /// <inheritdoc />
        public void Load(string name)
        {
            var lines = programRepository.Load(name);
            Lines = new SortedList<int, IStatement>(lines);
            LastUsedName = name;
        }

        /// <inheritdoc />
        public EvaluateResult Run()
        {
            if (programRunner != null)
                throw new RunTimeException(ErrorMessages.ProgramIsRunning);

            programRunner = new ProgramRunner(Lines);
            try
            {
                while (programRunner.MoveNext())
                    programRunner.CurrentStatement.Evaluate(this);

                if (programRunner.IsBreaked)
                    return new EvaluateResult(Messages.CtrlCPressed);

                return new EvaluateResult(Messages.ProgramCompleted);
            }
            catch (Exception exception)
            {
                var message = string.Format(ErrorMessages.RunTimeErrorOccured, exception.Message);
                return new EvaluateResult(message);
            }
            finally
            {
                programRunner = null;
            }
        }

        /// <inheritdoc />
        public void End()
        {
            if (programRunner == null)
                throw new RunTimeException(ErrorMessages.ProgramIsNotRunning);

            programRunner.Complete();
        }

        /// <inheritdoc />
        public void Goto(int number)
        {
            if (programRunner == null)
                throw new RunTimeException(ErrorMessages.ProgramIsNotRunning);

            programRunner.Goto(number);
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
            if (programRunner == null)
                return;

            programRunner.Break();
        }
    }
}
