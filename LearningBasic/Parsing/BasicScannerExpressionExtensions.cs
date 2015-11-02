namespace LearningBasic.Parsing
{
    using System;
    using System.Collections.Generic;
    using LearningBasic.Parsing.Ast;
    using LearningBasic.Parsing.Ast.Expressions;

    public static class BasicScannerExpressionExtensions
    {
        public static ILValue ReadLValue(this IScanner<Token> scanner)
        {
            ILValue result;
            if (scanner.TryReadLValue(out result))
                return result;

            throw new ParserException(ErrorMessages.MissingLValue);
        }

        public static bool TryReadLValue(this IScanner<Token> scanner, out ILValue result)
        {
            string identifier;
            if (scanner.TryReadToken(Token.Identifier, out identifier))
            {
                if (scanner.CurrentToken == Token.LParen)
                    throw new ParserException(ErrorMessages.MissingLValue);

                if (scanner.TryReadToken(Token.LBracket))
                {
                    var parameters = scanner.ReadExpressions();
                    scanner.ReadToken(Token.RBracket);
                    result = new ArrayVariable(identifier, parameters);
                    return true;
                }

                result = new ScalarVariable(identifier);
                return true;
            }

            result = null;
            return false;
        }

        public static IReadOnlyList<IExpression> ReadExpressions(this IScanner<Token> scanner)
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

        public static IExpression ReadExpression(this IScanner<Token> scanner)
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

        public static IExpression ReadAddOperand(this IScanner<Token> scanner)
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

        public static IExpression ReadMulOperand(this IScanner<Token> scanner)
        {
            if (scanner.TryReadToken(Token.Plus))
                return new Positive(scanner.ReadMulOperand());
            else if (scanner.TryReadToken(Token.Minus))
                return new Negative(scanner.ReadMulOperand());
            else return scanner.ReadUnaryOperand();

        }

        public static IExpression ReadUnaryOperand(this IScanner<Token> scanner)
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

        public static IExpression ReadValue(this IScanner<Token> scanner)
        {
            string text;
            if (scanner.TryReadToken(Token.Integer, out text))
                return new Constant(text);
            else if (scanner.TryReadToken(Token.Float, out text))
                return new Constant(text);
            else if (scanner.TryReadToken(Token.String, out text))
                return new Constant(text);
            else if (scanner.TryReadToken(Token.LParen))
            {
                var expression = scanner.ReadExpression();
                scanner.ReadToken(Token.RParen);
                return expression;
            }
            else if (scanner.TryReadToken(Token.Identifier, out text))
            {
                if (scanner.TryReadToken(Token.LBracket))
                {
                    var parameters = scanner.ReadExpressions();
                    scanner.ReadToken(Token.RBracket);
                    return new ArrayVariable(text, parameters);
                }
                else if (scanner.TryReadToken(Token.LParen))
                {
                    var parameters = scanner.ReadExpressions();
                    scanner.ReadToken(Token.RParen);
                    throw new NotImplementedException();
                }
                else
                    return new ScalarVariable(text);
            }
            else
                throw new ParserException(ErrorMessages.MissingTerminal);
        }
    }
}
