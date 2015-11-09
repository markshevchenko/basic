namespace LearningBasic.Parsing.Code.Statements
{
    using LearningBasic.RunTime;

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
