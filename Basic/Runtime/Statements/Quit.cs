namespace Basic.Runtime.Statements
{
    public class Quit : IStatement
    {
        public EvaluateResult Execute(RunTimeEnvironment rte)
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
