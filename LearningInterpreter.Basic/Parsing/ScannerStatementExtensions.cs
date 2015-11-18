namespace LearningInterpreter.Basic.Parsing
{
    using LearningInterpreter.Basic.Code;
    using LearningInterpreter.Basic.Code.Statements;
    using LearningInterpreter.Parsing;
    using LearningInterpreter.RunTime;

    /// <summary>
    /// Implements extension methods to parse BASIC statements.
    /// </summary>
    public static class ScannerStatementExtensions
    {
        public static IStatement ReadStatementExcludingNext(this IScanner<Token> scanner)
        {
            IStatement result;
            if (scanner.TryReadStatementExcludingNext(out result))
                return result;

            throw new ParserException(ErrorMessages.MissingStatement);
        }

        public static bool TryReadStatementExcludingNext(this IScanner<Token> scanner, out IStatement result)
        {
            if (scanner.TryReadLet(out result))
                return true;

            if (scanner.TryReadPrint(out result))
                return true;

            if (scanner.TryReadInput(out result))
                return true;

            if (scanner.TryReadList(out result))
                return true;

            if (scanner.TryReadRemove(out result))
                return true;

            if (scanner.TryReadSave(out result))
                return true;

            if (scanner.TryReadLoad(out result))
                return true;

            if (scanner.TryReadGoto(out result))
                return true;

            if (scanner.TryReadRandomize(out result))
                return true;

            if (scanner.TryReadRem(out result))
                return true;

            if (scanner.TryReadIfThenElse(out result))
                return true;

            if (scanner.TryReadDim(out result))
                return true;

            if (scanner.TryReadFor(out result))
                return true;

            if (scanner.TryReadToken(Token.Run, () => new Run(), out result))
                return true;

            if (scanner.TryReadToken(Token.End, () => new End(), out result))
                return true;

            if (scanner.TryReadToken(Token.Quit, () => new Quit(), out result))
                return true;

            result = null;
            return false;
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

        public static bool TryReadRandomize(this IScanner<Token> scanner, out IStatement result)
        {
            if (scanner.TryReadToken(Token.Randomize))
            {
                IExpression seed = scanner.ReadExpression();
                result = new Randomize(seed);
                return true;
            }

            result = null;
            return false;
        }

        public static bool TryReadRem(this IScanner<Token> scanner, out IStatement result)
        {
            if (scanner.TryReadToken(Token.Rem))
            {
                string comment;
                scanner.ReadToken(Token.Comment, out comment);
                result = new Rem(comment);
                return true;
            }

            result = null;
            return false;
        }

        public static bool TryReadIfThenElse(this IScanner<Token> scanner, out IStatement result)
        {
            if (scanner.TryReadToken(Token.If))
            {
                var condition = scanner.ReadCondition();
                scanner.ReadToken(Token.Then);
                var then = scanner.ReadStatementExcludingNext();

                if (scanner.TryReadToken(Token.Else))
                {
                    var @else = scanner.ReadStatementExcludingNext();
                    result = new IfThenElse(condition, then, @else);
                }
                else
                    result = new IfThenElse(condition, then);

                return true;
            }

            result = null;
            return false;
        }

        public static bool TryReadDim(this IScanner<Token> scanner, out IStatement result)
        {
            if (scanner.TryReadToken(Token.Dim))
            {
                var array = scanner.ReadArray();
                result = new Dim(array);
                return true;
            }

            result = null;
            return false;
        }

        public static bool TryReadFor(this IScanner<Token> scanner, out IStatement result)
        {
            if (scanner.TryReadToken(Token.For))
            {
                ILValue loopVariable = scanner.ReadLValue();
                scanner.ReadToken(Token.Eq);
                IExpression from = scanner.ReadExpression();
                scanner.ReadToken(Token.To);
                IExpression to = scanner.ReadExpression();

                IExpression step = null;
                if (scanner.TryReadToken(Token.Step))
                    step = scanner.ReadExpression();

                if (scanner.CurrentToken == Token.Next)
                    throw new ParserException(ErrorMessages.MissingStatement);

                IStatement statement;
                if (scanner.TryReadStatementExcludingNext(out statement))
                {
                    scanner.ReadToken(Token.Next);
                    result = new ForNext(loopVariable, from, to, step, statement);
                }
                else
                    result = new For(loopVariable, from, to, step);

                return true;
            }

            result = null;
            return false;
        }
    }
}
