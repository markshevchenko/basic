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

            var format = GetLineFormat(rte.Lines.Keys);
            var filterdLines = rte.Lines.Where(line => Range.Contains(line.Key));
            PrintLines(rte.InputOutput, format, filterdLines);

            return EvaluateResult.Empty;
        }

        public override string ToString()
        {
            if (Range.IsDefined)
                return "LIST";

            return "LIST " + Range;
        }

        public static void PrintLines(IInputOutput inputOutput, string format, IEnumerable<KeyValuePair<int, IStatement>> lines)
        {
            foreach (var line in lines)
            {
                var number = line.Key;
                var statement = line.Value;
                inputOutput.WriteLine(format, number, statement);
            }
        }

        public static string GetLineFormat(IList<int> sortedLineNumbers)
        {
            var maxLineNumber = sortedLineNumbers.Last();
            return GetEnoughWideFormat(maxLineNumber) + " {1}";
        }

        public static string GetEnoughWideFormat(int maxLineNumber)
        {
            if (maxLineNumber < 10)
                return "{0,1}";
            else if (maxLineNumber < 100)
                return "{0,2}";
            else if (maxLineNumber < 1000)
                return "{0,3}";
            else if (maxLineNumber < 10000)
                return "{0,4}";
            else
                return "{0,5}";
        }
    }
}
