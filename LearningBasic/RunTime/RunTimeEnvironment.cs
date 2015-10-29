namespace LearningBasic.RunTime
{
    using System;
    using System.Collections.Generic;

    public class RunTimeEnvironment : IRunTimeEnvironment
    {
        public IInputOutput InputOutput { get; private set; }

        public bool IsClosed { get; private set; }

        public IDictionary<string, dynamic> Variables { get; private set; }

        public SortedList<int, Statement> Statements { get; private set; }

        public RunTimeEnvironment(IInputOutput inputOutput)
        {
            if (inputOutput == null)
                throw new ArgumentNullException("inputOutput");

            InputOutput = inputOutput;
            IsClosed = false;
            Variables = new Dictionary<string, dynamic>();
            Statements = new SortedList<int, Statement>();
        }

        public void Close()
        {
            IsClosed = true;
        }
    }
}
