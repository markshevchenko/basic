namespace Basic.Runtime.Statements
{
    public class Remove : IStatement
    {
        public Range Range { get; private set; }

        public Remove(Range range)
        {
            if (range == Range.Undefined)
                throw new System.ArgumentException(ErrorMessages.RemoveMissingRange, "range");

            Range = range;
        }

        public EvaluateResult Execute(RunTimeEnvironment rte)
        {
            int start;
            int count;
            Range.GetBounds(rte, out start, out count);

            rte.RemoveRange(start, count);

            var message = string.Format(Messages.RemoveResult, count);
            return new EvaluateResult(message);
        }

        public override string ToString()
        {
            return "REMOVE " + Range.ToString();
        }
    }
}
