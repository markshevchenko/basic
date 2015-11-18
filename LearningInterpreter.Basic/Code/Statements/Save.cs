namespace LearningInterpreter.Basic.Code.Statements
{
    using System;
    using LearningInterpreter.Basic.Code.Expressions;
    using LearningInterpreter.RunTime;

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
                throw new ArgumentNullException("name");

            Name = name;
        }

        public EvaluateResult Execute(IRunTimeEnvironment rte)
        {
            string name = GetFileName(rte);
            rte.Save(name);

            var message = string.Format(Messages.ProgramSaved, rte.Variables.LastUsedProgramName);
            return new EvaluateResult(message);
        }

        private string GetFileName(IRunTimeEnvironment rte)
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
