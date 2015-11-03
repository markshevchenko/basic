namespace LearningBasic.Evaluating
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Implements the run-time environment.
    /// </summary>
    public class RunTimeEnvironment : IRunTimeEnvironment
    {
        private readonly IProgramRepository programRepository;

        /// <inheritdoc />
        public string LastUsedName { get; private set; }

        /// <inheritdoc />
        public IInputOutput InputOutput { get; private set; }

        /// <inheritdoc />
        public bool IsClosed { get; private set; }

        /// <inheritdoc />
        public IDictionary<string, dynamic> Variables { get; private set; }

        /// <inheritdoc />
        public SortedList<int, IStatement> Lines { get; private set; }

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
            LastUsedName = null;
            IsClosed = false;
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
    }
}
