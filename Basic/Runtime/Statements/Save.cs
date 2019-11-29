namespace Basic.Runtime.Statements
{
    using System;
    using Basic.Runtime.Expressions;

    public class Save : IStatement
    {
        public string Name { get; private set; }

        public Save()
        {
            Name = null;
        }

        public Save(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            Name = name;
        }

        public EvaluateResult Execute(RunTimeEnvironment rte)
        {
            string name = GetFileName(rte);
            rte.Save(name);

            var message = string.Format(Messages.ProgramSaved, rte.Variables.LastUsedProgramName);
            return new EvaluateResult(message);
        }

        private string GetFileName(RunTimeEnvironment rte)
        {
            if (Name == null)
            {
                if (rte.Variables.LastUsedProgramName == null)
                {
                    rte.InputOutput.Write(Messages.InputProgramName);
                    return rte.InputOutput.ReadLine();
                }
                else
                    return rte.Variables.LastUsedProgramName;
            }
            else
                return Name;
        }

        public override string ToString()
        {
            if (Name == null)
                return "SAVE";

            return "SAVE " + Constant.ToString(Name);
        }
    }
}
