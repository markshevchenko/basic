namespace LearningBasic.Parsing.Ast.Statements
{
    using System.Collections.Generic;
    using System.Linq;

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

        public EvaluateResult Run(IRunTimeEnvironment rte)
        {
            if (rte.Lines.Count == 0)
                return EvaluateResult.Empty;

            var filterdLines = rte.Lines.Where(line => Range.Contains(line.Key));
            var printableLines = LinesExtensions.ToPrintable(filterdLines);
            PrintLines(rte.InputOutput, printableLines);

            return EvaluateResult.Empty;
        }

        public override string ToString()
        {
            if (Range.IsDefined)
                return "LIST";

            return "LIST " + Range;
        }

        public static void PrintLines(IInputOutput inputOutput, IEnumerable<string> printableLines)
        {
            foreach (var printableLine in printableLines)
                inputOutput.WriteLine(printableLine);
        }
    }
}
