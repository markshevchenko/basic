namespace LearningInterpreter.Basic.Code.Statements
{
    using LearningInterpreter.RunTime;

    public class End : IStatement
    {
        public EvaluateResult Execute(IRunTimeEnvironment rte)
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
