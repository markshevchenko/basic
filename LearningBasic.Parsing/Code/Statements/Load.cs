namespace LearningBasic.Parsing.Code.Statements
{
    using System;
    using LearningBasic.Parsing.Code.Expressions;
    using LearningBasic.RunTime;

    public class Load : IStatement
    {
        public string Name { get; private set; }

        public Load(string name)
        {
            if (name == null)
                throw new ArgumentNullException("programName");

            Name = name;
        }

        public EvaluateResult Execute(IRunTimeEnvironment rte)
        {
            rte.Load(Name);

            var message = string.Format(Messages.ProgramLoaded, rte.LastUsedName);
            return new EvaluateResult(message);
        }

        public override string ToString()
        {
            return "LOAD " + Constant.ToString(Name);
        }
    }
}
