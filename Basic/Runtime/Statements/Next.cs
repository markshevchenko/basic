namespace Basic.Runtime.Statements
{
    public class Next : IStatement
    {
        public EvaluateResult Execute(RunTimeEnvironment rte)
        {
            rte.TakeLastLoopStep();

            if (rte.IsLastLoopOver)
                rte.StopLastLoop();
            else
                rte.RepeatLastLoop();

            return EvaluateResult.None;
        }
        public override string ToString()
        {
            return "NEXT";
        }
    }
}
