namespace LearningInterpreter.Basic.Code.Statements
{
    using LearningInterpreter.RunTime;

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
