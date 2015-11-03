namespace LearningBasic.Parsing.Ast.Statements
{
    public class Save : IStatement
    {
        public string ProgramName { get; private set; }

        public Save(string programName)
        {
            ProgramName = programName;
        }

        public EvaluateResult Run(IRunTimeEnvironment rte)
        {
            if (ProgramName == null)
                rte.Save();
            else
                rte.Save(ProgramName);

            var message = string.Format(Messages.ProgramSaved, rte.LastUsedName);
            return new EvaluateResult(message);
        }

        public override string ToString()
        {
            if (ProgramName == null)
                return "SAVE";

            return "SAVE " + ProgramName.ToPrintable();
        }
    }
}
