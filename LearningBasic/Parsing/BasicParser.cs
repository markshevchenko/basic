namespace LearningBasic.Parsing
{
    using System;
    using System.IO;

    public class BasicParser : IParser<Tag>
    {
        private readonly IScannerFactory<Token> scannerFactory;

        public BasicParser(IScannerFactory<Token> scannerFactory)
        {
            if (scannerFactory == null)
                throw new ArgumentNullException("scannerFactory");

            this.scannerFactory = scannerFactory;
        }

        public virtual AstNode<Tag> Parse(string line)
        {
            var inputStream = new StringReader(line);
            using (var scanner = this.scannerFactory.Create(inputStream))
            {
                string lineNumber;
                AstNode<Tag> statement;

                if (scanner.TryReadToken(Token.Integer, out lineNumber))
                {
                    if (scanner.TryParseNextStatement(out statement))
                        return new AstNode<Tag>(Tag.Line, lineNumber, statement);
                    
                    statement = scanner.ParseStatement();
                    return new AstNode<Tag>(Tag.Line, lineNumber, statement);
                }

                statement = scanner.ParseStatement();
                return new AstNode<Tag>(Tag.Line, statement);
            }
        }

    }
}
