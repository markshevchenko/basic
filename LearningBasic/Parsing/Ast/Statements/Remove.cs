namespace LearningBasic.Parsing.Ast.Statements
{
    using System;
    using System.Linq;

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
            var numbersToRemove = rte.Lines
                                     .Select(line => line.Key)
                                     .Where(Range.Contains)
                                     .ToArray();

            foreach (var numberToRemove in numbersToRemove)
                rte.Lines.Remove(numberToRemove);

            var message = string.Format(Messages.RemoveResult, numbersToRemove.Length);
            return new EvaluateResult(message);
        }

        public override string ToString()
        {
            return "REMOVE " + Range.ToString();
        }
    }
}
