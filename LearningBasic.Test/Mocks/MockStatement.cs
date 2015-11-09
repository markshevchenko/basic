namespace LearningBasic.Mocks
{
    using System;
    using LearningBasic.RunTime;

    public class MockStatement : IStatement
    {
        private readonly Action action;
        private readonly EvaluateResult result;

        public MockStatement()
        {
            this.action = () => { };
            this.result = EvaluateResult.None;
        }

        public MockStatement(EvaluateResult result)
        {
            this.action = () => { };
            this.result = result;
        }

        public MockStatement(Action action)
        {
            this.action = action;
            this.result = EvaluateResult.None;
        }

        public EvaluateResult Execute(IRunTimeEnvironment rte)
        {
            action();
            return result;
        }
    }
}
