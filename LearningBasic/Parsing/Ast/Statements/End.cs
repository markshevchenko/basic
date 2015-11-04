namespace LearningBasic.Parsing.Ast.Statements
{
    public class End : IStatement
    {
        public EvaluateResult Execute(IRunTimeEnvironment rte)
        {
            rte.End();

            return EvaluateResult.Empty;
        }

        public override string ToString()
        {
            return "END";
        }
    }
}
