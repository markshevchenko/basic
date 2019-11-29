namespace Basic.Parsing
{
    using System;
    using System.Collections.Generic;
    using Basic.Runtime;
    using Basic.Runtime.Expressions;

    /// <summary>
    /// Implements extension methods to parse BASIC expressions.
    /// </summary>
    public static class ScannerExpressionExtensions
    {
        public static ILValue ReadLValue(this Scanner scanner)
        {
            ILValue result;
            if (scanner.TryReadLValue(out result))
                return result;

            throw new ParserException(ErrorMessages.MissingLValue);
        }

        public static bool TryReadLValue(this Scanner scanner, out ILValue result)
        {
            string identifier;
            if (scanner.TryReadToken(Token.Identifier, out identifier))
            {
                if (scanner.CurrentToken == Token.LParen)
                    throw new ParserException(ErrorMessages.MissingLValue);

                if (scanner.TryReadToken(Token.LBracket))
                {
                    var indexes = scanner.ReadExpressions();
                    scanner.ReadToken(Token.RBracket);
                    result = new ArrayVariable(identifier, indexes);
                    return true;
                }

                result = new ScalarVariable(identifier);
                return true;
            }

            result = null;
            return false;
        }

        public static ArrayVariable ReadArray(this Scanner scanner)
        {
            string identifier;
            scanner.ReadToken(Token.Identifier, out identifier);
            scanner.ReadToken(Token.LBracket);
            var indexes = scanner.ReadExpressions();
            scanner.ReadToken(Token.RBracket);
            return new ArrayVariable(identifier, indexes);
        }

        public static IReadOnlyList<IExpression> ReadExpressions(this Scanner scanner)
        {
            var result = new List<IExpression>();

            do
            {
                var expression = scanner.ReadExpression();
                result.Add(expression);
            }
            while (scanner.TryReadToken(Token.Comma));

            return result;
        }

        public static IExpression ReadExpression(this Scanner scanner)
        {
            var result = scanner.ReadAddOperand();

            while (true)
            {
                IExpression tail;
                if (scanner.TryReadToken(Token.Plus))
                {
                    tail = scanner.ReadAddOperand();
                    result = new Add(result, tail);
                }
                else if (scanner.TryReadToken(Token.Minus))
                {
                    tail = scanner.ReadAddOperand();
                    result = new Subtract(result, tail);
                }
                else
                    return result;
            }
        }

        public static IExpression ReadAddOperand(this Scanner scanner)
        {
            var result = scanner.ReadMulOperand();

            while (true)
            {
                IExpression tail;
                if (scanner.TryReadToken(Token.Asterisk))
                {
                    tail = scanner.ReadMulOperand();
                    result = new Multiply(result, tail);
                }
                else if (scanner.TryReadToken(Token.Slash))
                {
                    tail = scanner.ReadMulOperand();
                    result = new Divide(result, tail);
                }
                else if (scanner.TryReadToken(Token.Mod))
                {
                    tail = scanner.ReadMulOperand();
                    result = new Modulo(result, tail);
                }
                else
                    return result;
            }
        }

        public static IExpression ReadMulOperand(this Scanner scanner)
        {
            if (scanner.TryReadToken(Token.Plus))
                return new Positive(scanner.ReadMulOperand());
            else if (scanner.TryReadToken(Token.Minus))
                return new Negative(scanner.ReadMulOperand());
            else
                return scanner.ReadUnaryOperand();
        }

        public static IExpression ReadUnaryOperand(this Scanner scanner)
        {
            var result = scanner.ReadValue();

            if (scanner.TryReadToken(Token.Caret))
            {
                IExpression tail;
                if (scanner.CurrentToken == Token.Plus || scanner.CurrentToken == Token.Minus)
                    tail = scanner.ReadMulOperand();
                else
                    tail = scanner.ReadUnaryOperand();

                result = new Power(result, tail);
            }

            return result;
        }

        private static IExpression ReadValue(this Scanner scanner)
        {
            if (scanner.TryReadConstant(out IExpression expression))
                return expression;

            if (scanner.TryReadParenthesizedExpression(out expression))
                return expression;

            if (scanner.TryReadArrayFunctionOrScalar(out expression))
                return expression;

            throw new ParserException(ErrorMessages.MissingTerminal);
        }

        private static bool TryReadConstant(this Scanner scanner, out IExpression expression)
        {
            if (scanner.TryReadToken(Token.Integer, out string text)
                || scanner.TryReadToken(Token.Float, out text)
                || scanner.TryReadToken(Token.String, out text))
            {
                expression = new Constant(text);
                return true;
            }

            expression = null;
            return false;
        }

        private static bool TryReadParenthesizedExpression(this Scanner scanner, out IExpression expression)
        {
            if (scanner.TryReadToken(Token.LParen))
            {
                expression = scanner.ReadExpression();
                scanner.ReadToken(Token.RParen);
                return true;
            }

            expression = null;
            return false;
        }

        private static bool TryReadArrayFunctionOrScalar(this Scanner scanner, out IExpression expression)
        {
            if (scanner.TryReadToken(Token.Identifier, out string text))
            {
                if (scanner.TryReadToken(Token.LBracket))
                {
                    var parameters = scanner.ReadExpressions();
                    scanner.ReadToken(Token.RBracket);
                    expression = new ArrayVariable(text, parameters);
                    return true;
                }

                if (scanner.TryReadToken(Token.LParen))
                {
                    IReadOnlyList<IExpression> parameters;

                    if (scanner.TryReadToken(Token.RParen))
                    {
                        parameters = new IExpression[0];
                    }
                    else
                    {
                        parameters = scanner.ReadExpressions();
                        scanner.ReadToken(Token.RParen);
                    }

                    if (string.Equals("RND", text, StringComparison.OrdinalIgnoreCase))
                        expression = new Rnd(parameters);
                    else
                        expression = new Function(text, parameters);

                    return true;
                }
                
                expression = new ScalarVariable(text);
                return true;
            }

            expression = null;
            return false;
        }
    }
}
