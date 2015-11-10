namespace LearningBasic.Parsing.Code.Statements
{
    using LearningBasic.RunTime;

    public class Nop : IStatement
    {
        public EvaluateResult Execute(IRunTimeEnvironment rte)
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
