namespace LearningBasic.Parsing.Ast.Statements
{
    public class Quit : IStatement
    {
        public EvaluateResult Execute(IRunTimeEnvironment rte)
        {
            rte.Close();

            return new EvaluateResult(Messages.BasicInterpreterTerminated);
        }

        public override string ToString()
        {
            return "QUIT";
        }
    }
}
