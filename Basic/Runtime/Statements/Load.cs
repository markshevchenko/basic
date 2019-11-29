namespace Basic.Runtime.Statements
{
    using System;
    using Basic.Runtime.Expressions;

    public class Load : IStatement
    {
        public string Name { get; private set; }

        public Load(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            Name = name;
        }

        public EvaluateResult Execute(RunTimeEnvironment rte)
        {
            rte.Load(Name);

            var message = string.Format(Messages.ProgramLoaded, rte.Variables.LastUsedProgramName);
            return new EvaluateResult(message);
        }

        public override string ToString()
        {
            return "LOAD " + Constant.ToString(Name);
        }
    }
}
