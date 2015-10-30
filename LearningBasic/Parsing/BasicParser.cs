namespace LearningBasic.Parsing
{
    using System;
    using System.IO;
    using LearningBasic.Parsing.Ast;
    using LearningBasic.Parsing.Ast.Statements;

    public class BasicParser : ILineParser
    {
        private readonly IScannerFactory<Token> scannerFactory;

        public BasicParser(IScannerFactory<Token> scannerFactory)
        {
            if (scannerFactory == null)
                throw new ArgumentNullException("scannerFactory");

            this.scannerFactory = scannerFactory;
        }

        public ILine Parse(string line)
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
