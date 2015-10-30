namespace LearningBasic.Parsing.Ast.Statements
{
    public class Quit : IStatement
    {
        public Result Run(IRunTimeEnvironment rte)
        {
            rte.Close();
            return Result.Nothing;
        }

        public override string ToString()
        {
            return "QUIT";
        }
    }
}
