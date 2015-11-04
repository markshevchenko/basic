using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningBasic.Test.Mocks
{
    public class MockStatement : IStatement
    {
        private readonly Action action;

        public MockStatement()
        {
            this.action = () => { };
        }

        public MockStatement(Action action)
        {
            this.action = action;
        }

        public EvaluateResult Execute(IRunTimeEnvironment rte)
        {
            action();
            return EvaluateResult.Empty;
        }
    }
}
