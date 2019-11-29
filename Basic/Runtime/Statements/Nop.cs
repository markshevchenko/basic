namespace Basic.Runtime.Statements
{
    public class Nop : IStatement
    {
        public EvaluateResult Execute(RunTimeEnvironment rte)
        {
            // No operation statement does nothing.
            return EvaluateResult.None;
        }

        public override string ToString()
        {
            return string.Empty;
        }
    }
}
