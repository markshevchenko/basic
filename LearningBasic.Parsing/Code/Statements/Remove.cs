namespace LearningBasic.Parsing.Code.Statements
{
    using System;
    using System.Linq;
    using LearningBasic.RunTime;

    public class Remove : IStatement
    {
        public Range Range { get; private set; }

        public Remove(Range range)
        {
            if (range == Range.Undefined)
                throw new ArgumentException(ErrorMessages.RemoveMissingRange, "range");

            Range = range;
        }

        public EvaluateResult Execute(IRunTimeEnvironment rte)
        {
            var from = new Line(Range.Min.ToString(), new StubStatement());
            var to = new Line(Range.Max.ToString(), new StubStatement());

            int count = rte.Remove(from, to);

            var message = string.Format(Messages.RemoveResult, count);
            return new EvaluateResult(message);
        }

        public override string ToString()
        {
            return "REMOVE " + Range.ToString();
        }

        private class StubStatement : IStatement
        {
            public EvaluateResult Execute(IRunTimeEnvironment rte)
            {
                throw new NotImplementedException();
            }
        }
    }
}
