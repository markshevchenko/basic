namespace LearningBasic.Parsing.Code.Statements
{
    using System.Linq;
    using LearningBasic.RunTime;

    public class List : IStatement
    {
        public Range Range { get; private set; }

        public List()
        {
            Range = Range.Undefined;
        }

        public List(Range range)
        {
            Range = range;
        }

        public EvaluateResult Execute(IRunTimeEnvironment rte)
        {
            if (rte.Lines.Count == 0)
                return EvaluateResult.None;

            var filterdLines = rte.Lines.OfType<Line>()
                                        .Where(line => Range.Contains(line.Number.Value));

            foreach (var line in filterdLines)
                rte.InputOutput.WriteLine(line.ToString());

            return EvaluateResult.None;
        }

        public override string ToString()
        {
            if (Range.IsDefined)
                return "LIST";

            return "LIST " + Range;
        }
    }
}
