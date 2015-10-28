namespace LearningBasic
{
    using System;
    using System.Collections.Generic;

    public class RunTimeEnvironment : IRunTimeEnvironment
    {
        public IInputOutput InputOutput { get; private set; }

        public bool IsTerminated { get; private set; }

        public IDictionary<string, dynamic> Variables { get; private set; }

        public RunTimeEnvironment(IInputOutput inputOutput)
        {
            if (inputOutput == null)
                throw new ArgumentNullException("inputOutput");

            InputOutput = inputOutput;
            IsTerminated = false;
            Variables = new Dictionary<string, dynamic>();
        }

        public void AddCommand(int lineNumber, Command command)
        {

        }

        public void Terminate()
        {
            IsTerminated = true;
        }
    }
}
