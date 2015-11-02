namespace LearningBasic
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Implements the run-time environment.
    /// </summary>
    public class RunTimeEnvironment : IRunTimeEnvironment
    {
        public string ProgramName { get; set; }

        /// <inheritdoc />
        public IInputOutput InputOutput { get; private set; }

        /// <inheritdoc />
        public IProgramRepository ProgramRepository { get; private set; }

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

            ProgramName = null;
            InputOutput = inputOutput;
            ProgramRepository = programRepository;
            IsClosed = false;
            Variables = new Dictionary<string, dynamic>();
            Lines = new SortedList<int, IStatement>();
        }

        /// <inheritdoc />
        public void Close()
        {
            IsClosed = true;
        }
    }
}
