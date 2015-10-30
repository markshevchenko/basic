namespace LearningBasic.Parsing.Ast.Statements
{
    public class Quit : IStatement
    {
        public StatementResult Run(IRunTimeEnvironment rte)
        {
            rte.Close();
            return StatementResult.Empty;
        }

        public override string ToString()
        {
            return "QUIT";
        }
    }
}
