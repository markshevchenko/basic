namespace Basic.Tests.Mocks
{
    using System;
    using Basic.Runtime;

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

        public EvaluateResult Execute(RunTimeEnvironment rte)
        {
            action();
            return result;
        }
    }
}
