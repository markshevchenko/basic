namespace LearningBasic.Parsing.Ast.Statements
{
    using System;

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

            var message = string.Format(Messages.ProgramSaved, rte.LastUsedName);
            return new EvaluateResult(message);
        }

        private string GetFileName(IRunTimeEnvironment rte)
        {
            if (Name == null)
            {
                if (rte.LastUsedName == null)
                {
                    rte.InputOutput.Write(Messages.InputProgramName);
                    return rte.InputOutput.ReadLine();
                }
                else
                    return rte.LastUsedName;
            }
            else
                return Name;
        }

        public override string ToString()
        {
            if (Name == null)
                return "SAVE";

            return "SAVE " + Name.ToPrintable();
        }
    }
}
