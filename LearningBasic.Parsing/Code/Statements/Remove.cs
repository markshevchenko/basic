namespace LearningBasic.Parsing.Code.Statements
{
    using System;
    using System.Linq;
    using LearningBasic.RunTime;
    using System.Globalization;

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
            var lowLine = new Line(Range.Min, new Nop());
            var highLine = new Line(Range.Min, new Nop());

            var lowIndex = rte.BinarySearch(lowLine);
            var highIndex = rte.BinarySearch(highLine);

            if (lowIndex < 0)
                lowIndex = ~lowIndex;

            if (highIndex < 0)
                highIndex = ~highIndex - 1;

            int count = highIndex - lowIndex + 1;

            rte.RemoveRange(lowIndex, count);

            var message = string.Format(Messages.RemoveResult, count);
            return new EvaluateResult(message);
        }

        public override string ToString()
        {
            return "REMOVE " + Range.ToString();
        }
    }
}
