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

        public EvaluateResult Run(IRunTimeEnvironment rte)
        {
            if (Name == null)
                rte.Save();
            else
                rte.Save(Name);

            var message = string.Format(Messages.ProgramSaved, rte.LastUsedName);
            return new EvaluateResult(message);
        }

        public override string ToString()
        {
            if (Name == null)
                return "SAVE";

            return "SAVE " + Name.ToPrintable();
        }
    }
}
