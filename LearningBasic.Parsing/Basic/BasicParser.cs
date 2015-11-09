namespace LearningBasic.Parsing.Basic
{
    using System;
    using System.IO;
    using LearningBasic.Parsing.Code.Statements;
    using LearningBasic.RunTime;

    /// <summary>
    /// Implements BASIC per-line parser.
    /// </summary>
    public class BasicParser : ILineParser
    {
        private readonly IScannerFactory<Token> scannerFactory;

        /// <summary>
        /// Initializes an instance of the <see cref="BasicParser"/> with the specified scanner factory.
        /// </summary>
        /// <param name="scannerFactory">The factory of BASIC scanners.</param>
        public BasicParser(IScannerFactory<Token> scannerFactory)
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
            if (scanner.TryReadToken(Token.Next))
                return new Line(lineNumber, new Next());

            var statement = scanner.ReadStatementExcludingNext();
            return new Line(lineNumber, statement);
        }

        private static Line ReadStatemement(IScanner<Token> scanner)
        {
            var statement = scanner.ReadStatementExcludingNext();
            return new Line(statement);
        }
    }
}
