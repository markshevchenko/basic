namespace Basic.Parsing
{
    using Basic.Runtime;
    using Basic.Runtime.Statements;

    /// <summary>
    /// Implements BASIC-specific parser.
    /// </summary>
    public class Parser
    {
        /// <summary>
        /// Parses BASIC source line and builds parsed <see cref="Line">line</see>.
        /// </summary>
        /// <param name="line">The BASIC source line.</param>
        /// <returns>The parsed line.</returns>
        public Line Parse(string line)
        {
            var scanner = new Scanner(line);
            string lineNumber;

            if (scanner.TryReadToken(Token.Integer, out lineNumber))
                return ReadStatementWithLineNumber(scanner, lineNumber);

            return ReadStatemement(scanner);
        }

        private static Line ReadStatementWithLineNumber(Scanner scanner, string lineNumber)
        {
            IStatement statement;
            if (!scanner.TryReadToken(Token.Next, () => new Next(), out statement))
                statement = scanner.ReadStatementExcludingNext();

            return new Line(lineNumber, statement);
        }

        private static Line ReadStatemement(Scanner scanner)
        {
            var statement = scanner.ReadStatementExcludingNext();
            return new Line(statement);
        }
    }
}
