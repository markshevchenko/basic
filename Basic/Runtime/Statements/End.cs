namespace Basic.Runtime.Statements
{
    public class End : IStatement
    {
        public EvaluateResult Execute(RunTimeEnvironment rte)
        {
            rte.End();

            return EvaluateResult.None;
        }

        public override string ToString()
        {
            return "END";
        }
    }
}
