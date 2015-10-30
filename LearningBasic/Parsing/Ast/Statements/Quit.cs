namespace LearningBasic.Parsing.Ast.Statements
{
    public class Quit : IStatement
    {
        public EvaluateResult Run(IRunTimeEnvironment rte)
        {
            rte.Close();
            return EvaluateResult.Empty;
        }

        public override string ToString()
        {
            return "QUIT";
        }
    }
}
