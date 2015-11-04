namespace LearningBasic.Parsing
{
    using LearningBasic.Parsing.Ast;
    using LearningBasic.Parsing.Ast.Statements;

    /// <summary>
    /// Implements methods to parse BASIC statements and to build Abstract Syntax Tree's nodes.
    /// </summary>
    public static class BasicScannerStatementExtensions
    {
        public static IStatement ReadStatementExcludingNext(this IScanner<Token> scanner)
        {
            IStatement result;

            if (scanner.TryReadLet(out result))
                return result;

            if (scanner.TryReadPrint(out result))
                return result;

            if (scanner.TryReadInput(out result))
                return result;

            if (scanner.TryReadList(out result))
                return result;

            if (scanner.TryReadRemove(out result))
                return result;

            if (scanner.TryReadSave(out result))
                return result;

            if (scanner.TryReadLoad(out result))
                return result;

            if (scanner.TryReadGoto(out result))
                return result;

            if (scanner.TryReadToken(Token.Run))
                return new Run();

            if (scanner.TryReadToken(Token.End))
                return new End();

            if (scanner.TryReadToken(Token.Quit))
                return new Quit();

            throw new ParserException(ErrorMessages.MissingStatement);
        }

        public static bool TryReadLet(this IScanner<Token> scanner, out IStatement result)
        {
            ILValue lValue;
            IExpression expression;

            if (scanner.TryReadToken(Token.Let))
            {
                lValue = scanner.ReadLValue();
            }
            else if (!scanner.TryReadLValue(out lValue))
            {
                result = null;
                return false;
            }

            scanner.ReadToken(Token.Eq);
            expression = scanner.ReadExpression();
            result = new Let(lValue, expression);
            return true;
        }

        public static bool TryReadPrint(this IScanner<Token> scanner, out IStatement result)
        {
            if (scanner.TryReadToken(Token.Print))
            {
                var expressions = scanner.ReadExpressions();

                if (scanner.TryReadToken(Token.Semicolon))
                    result = new Print(expressions);
                else
                    result = new PrintLine(expressions);

                return true;
            }

            result = null;
            return false;
        }

        public static bool TryReadInput(this IScanner<Token> scanner, out IStatement result)
        {
            if (scanner.TryReadToken(Token.Input))
            {
                string prompt;
                if (scanner.TryReadToken(Token.String, out prompt))
                {
                    scanner.ReadToken(Token.Comma);
                    result = new Input(prompt, scanner.ReadLValue());
                    return true;
                }

                result = new Input(scanner.ReadLValue());
                return true;
            }

            result = null;
            return false;
        }

        public static bool TryReadList(this IScanner<Token> scanner, out IStatement result)
        {
            if (scanner.TryReadToken(Token.List))
            {
                Range range;
                if (scanner.TryReadRange(out range))
                    result = new List(range);
                else
                    result = new List();

                return true;
            }

            result = null;
            return false;
        }

        public static bool TryReadRange(this IScanner<Token> scanner, out Range result)
        {
            string text;
            if (scanner.TryReadToken(Token.Integer, out text))
            {
                int min = Line.Parse(text);

                if (scanner.TryReadToken(Token.Minus))
                {
                    scanner.ReadToken(Token.Integer, out text);
                    int max = Line.Parse(text);
                    result = new Range(min, max);
                }
                else
                    result = new Range(min);

                return true;
            }

            result = Range.Undefined;
            return false;
        }

        public static Range ReadRange(this IScanner<Token> scanner)
        {
            Range result;
            if (scanner.TryReadRange(out result))
                return result;

            throw new ParserException(ErrorMessages.RemoveMissingRange);
        }

        public static bool TryReadRemove(this IScanner<Token> scanner, out IStatement result)
        {
            if (scanner.TryReadToken(Token.Remove))
            {
                var range = scanner.ReadRange();
                result = new Remove(range);
                return true;
            }

            result = null;
            return false;
        }

        public static bool TryReadSave(this IScanner<Token> scanner, out IStatement result)
        {
            if (scanner.TryReadToken(Token.Save))
            {
                string fileName;
                if (scanner.TryReadToken(Token.String, out fileName))
                    result = new Save(fileName);
                else
                    result = new Save();

                return true;
            }

            result = null;
            return false;
        }

        public static bool TryReadLoad(this IScanner<Token> scanner, out IStatement result)
        {
            if (scanner.TryReadToken(Token.Load))
            {
                string fileName;
                scanner.ReadToken(Token.String, out fileName);
                result = new Load(fileName);
                return true;
            }

            result = null;
            return false;
        }

        public static bool TryReadGoto(this IScanner<Token> scanner, out IStatement result)
        {
            if (scanner.TryReadToken(Token.Goto))
            {
                IExpression number = scanner.ReadExpression();
                result = new Goto(number);
                return true;
            }

            result = null;
            return false;
        }
    }
}
