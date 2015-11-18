namespace LearningInterpreter.Basic.Code.Statements
{
    using System.Linq;
    using LearningInterpreter.Parsing;
    using LearningInterpreter.RunTime;

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
            int start;
            int count;
            Range.GetBounds(rte, out start, out count);
            var filteredLines = rte.Lines.Skip(start).Take(count);

            foreach (var line in filteredLines)
                rte.InputOutput.WriteLine(line.ToString());

            return EvaluateResult.None;
        }

        public override string ToString()
        {
            if (Range.IsDefined)
                return "LIST " + Range;

            return "LIST";
        }
    }
}
