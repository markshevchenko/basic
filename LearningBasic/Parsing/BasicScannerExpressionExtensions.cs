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
            var left = scanner.ReadAddOperand();

            while (true)
            {
                if (scanner.TryReadToken(Token.Plus))
                {
                    var right = scanner.ReadAddOperand();
                    left = new Add(left, right);
                }
                else if (scanner.TryReadToken(Token.Minus))
                {
                    var right = scanner.ReadAddOperand();
                    left = new Subtract(left, right);
                }
                else
                    return left;
            }
        }

        public static IExpression ReadAddOperand(this IScanner<Token> scanner)
        {
            var left = scanner.ReadMulOperand();

            while (true)
            {
                if (scanner.TryReadToken(Token.Asterisk))
                {
                    var right = scanner.ReadMulOperand();
                    left = new Multiply(left, right);
                }
                else if (scanner.TryReadToken(Token.Slash))
                {
                    var right = scanner.ReadMulOperand();
                    left = new Divide(left, right);
                }
                else if (scanner.TryReadToken(Token.Percent))
                {
                    var right = scanner.ReadMulOperand();
                    left = new Modulo(left, right);
                }
                else
                    return left;
            }
        }

        public static IExpression ReadMulOperand(this IScanner<Token> scanner)
        {
            var signStack = new Stack<Token>();

            while (scanner.CurrentToken == Token.Plus || scanner.CurrentToken == Token.Minus)
            {
                signStack.Push(scanner.CurrentToken);
                scanner.MoveNext();
            }

            var operand = scanner.ReadPowOperand();

            while (signStack.Count > 0)
            {
                if (signStack.Pop() == Token.Plus)
                    operand = new Positive(operand);
                else
                    operand = new Negative(operand);
            }

            return operand;
        }

        public static IExpression ReadPowOperand(this IScanner<Token> scanner)
        {
            var operandStack = new Stack<IExpression>();

            while (true)
            {
                operandStack.Push(scanner.ReadTerminal());

                if (!scanner.TryReadToken(Token.Caret))
                    break;
            }

            var right = operandStack.Pop();

            while (operandStack.Count > 0)
            {
                var left = operandStack.Pop();
                right = new Power(left, right);
            }

            return right;
        }

        public static IExpression ReadTerminal(this IScanner<Token> scanner)
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
