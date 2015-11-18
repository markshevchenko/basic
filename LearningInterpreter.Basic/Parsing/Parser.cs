namespace LearningInterpreter.Basic.Parsing
{
    using System;
    using System.IO;
    using LearningInterpreter.Basic.Code;
    using LearningInterpreter.Basic.Code.Statements;
    using LearningInterpreter.RunTime;
    using LearningInterpreter.Parsing;

    /// <summary>
    /// Implements BASIC-specific parser.
    /// </summary>
    public class Parser : ILineParser
    {
        private readonly IScannerFactory<Token> scannerFactory;

        /// <summary>
        /// Initializes an instance of the <see cref="Parser"/> with the specified scanner factory.
        /// </summary>
        /// <param name="scannerFactory">The factory of BASIC scanners.</param>
        public Parser(IScannerFactory<Token> scannerFactory)
        {
            if (scannerFactory == null)
                throw new ArgumentNullException("scannerFactory");

            this.scannerFactory = scannerFactory;
        }

        /// <inheritdoc />
        ILine ILineParser.Parse(string line)
        {
            return Parse(line);
        }

        /// <summary>
        /// Parses BASIC source line and builds parsed <see cref="ILine">line</see>.
        /// </summary>
        /// <param name="line">The BASIC source line.</param>
        /// <returns>The parsed line.</returns>
        public Line Parse(string line)
        {
            var inputStream = new StringReader(line);
            using (var scanner = this.scannerFactory.Create(inputStream))
            {
                string lineNumber;

                if (scanner.TryReadToken(Token.Integer, out lineNumber))
                    return ReadStatementWithLineNumber(scanner, lineNumber);

                return ReadStatemement(scanner);
            }
        }

        private static Line ReadStatementWithLineNumber(IScanner<Token> scanner, string lineNumber)
        {
            IStatement statement;
            if (!scanner.TryReadToken(Token.Next, () => new Next(), out statement))
                statement = scanner.ReadStatementExcludingNext();

            return new Line(lineNumber, statement);
        }

        private static Line ReadStatemement(IScanner<Token> scanner)
        {
            var statement = scanner.ReadStatementExcludingNext();
            return new Line(statement);
        }
    }
}
