namespace LearningInterpreter.Parsing
{
    using LearningInterpreter.Basic.Code;
    using LearningInterpreter.RunTime;
    using LearningInterpreter.Basic.Code.Statements;

    public static class RangeExtensions
    {
        public static void GetBounds(this Range range, IRunTimeEnvironment rte, out int start, out int count)
        {
            if (range.IsDefined)
            {
                var lowLine = new Line(range.Min, new Nop());
                var highLine = new Line(range.Max, new Nop());

                var lowIndex = rte.BinarySearch(lowLine);
                var highIndex = rte.BinarySearch(highLine);

                if (lowIndex < 0)
                    lowIndex = ~lowIndex;

                if (highIndex < 0)
                    highIndex = ~highIndex - 1;

                start = lowIndex;
                count = highIndex - lowIndex + 1;
            }
            else
            {
                start = 0;
                count = rte.Lines.Count;
            }
        }
    }
}
