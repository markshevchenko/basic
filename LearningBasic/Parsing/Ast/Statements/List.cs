namespace LearningBasic.Parsing.Ast.Statements
{
    using System.Collections.Generic;
    using System.Linq;

    public class List : IStatement
    {
        public StatementResult Run(IRunTimeEnvironment rte)
        {
            if (rte.Lines.Count == 0)
                return StatementResult.Empty;

            var format = GetLineFormat(rte.Lines.Keys);
            PrintLines(rte.InputOutput, format, rte.Lines);


            return StatementResult.Empty;
        }

        public override string ToString()
        {
            return "LIST";
        }

        public static void PrintLines(IInputOutput inputOutput, string format, IDictionary<int, IStatement> lines)
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
