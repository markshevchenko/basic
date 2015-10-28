namespace LearningBasic.Parsing
{
    using System.Collections.Generic;

    /// <summary>
    /// Implements methods to parse BASIC statements and to build Abstract Syntax Tree's nodes.
    /// </summary>
    public static class BasicScannerExtensions
    {
        #region Parse methods

        public static AstNode<Tag> ParseStatement(this IScanner<Token> scanner)
        {
            AstNode<Tag> result;

            if (scanner.TryParseLetStatement(out result))
                return result;

            if (scanner.TryParseInputStatement(out result))
                return result;

            if (scanner.TryParsePrintStatement(out result))
                return result;

            if (scanner.TryParseQuitStatement(out result))
                return result;

            throw new ParserException(ErrorMessages.MissingStatement);
        }

        public static AstNode<Tag> ParseLValue(this IScanner<Token> scanner)
        {
            AstNode<Tag> result;
            if (scanner.TryParseLValue(out result))
                return result;

            throw new ParserException(ErrorMessages.MissingLValue);
        }

        public static IReadOnlyCollection<AstNode<Tag>> ParseExpressions(this IScanner<Token> scanner)
        {
            var result = new List<AstNode<Tag>>();

            do
            {
                result.Add(scanner.ParseExpression());
            }
            while (scanner.TryReadToken(Token.Comma));

            return result;
        }

        public static AstNode<Tag> ParseExpression(this IScanner<Token> scanner)
        {
            var result = scanner.ParseAddOperand();

            while (true)
            {
                if (scanner.TryReadToken(Token.Plus))
                {
                    var operand = scanner.ParseAddOperand();
                    result = new AstNode<Tag>(Tag.Add, result, operand);
                }
                else if (scanner.TryReadToken(Token.Minus))
                {
                    var operand = scanner.ParseAddOperand();
                    result = new AstNode<Tag>(Tag.Subtract, result, operand);
                }
                else
                    return result;
            }
        }

        public static AstNode<Tag> ParseAddOperand(this IScanner<Token> scanner)
        {
            var result = scanner.ParseMulOperand();

            while (true)
            {
                if (scanner.TryReadToken(Token.Asterisk))
                {
                    var operand = scanner.ParseMulOperand();
                    result = new AstNode<Tag>(Tag.Multiply, result, operand);
                }
                else if (scanner.TryReadToken(Token.Slash))
                {
                    var operand = scanner.ParseMulOperand();
                    result = new AstNode<Tag>(Tag.Divide, result, operand);
                }
                else if (scanner.TryReadToken(Token.Percent))
                {
                    var operand = scanner.ParseMulOperand();
                    result = new AstNode<Tag>(Tag.Modulo, result, operand);
                }
                else
                    return result;
            }
        }

        public static AstNode<Tag> ParseMulOperand(this IScanner<Token> scanner)
        {
            var signStack = new Stack<Tag>();

            while (true)
            {
                if (scanner.TryReadToken(Token.Plus))
                    signStack.Push(Tag.Positive);
                else if (scanner.TryReadToken(Token.Minus))
                    signStack.Push(Tag.Negative);
                else
                    break;
            }

            var result = scanner.ParsePowOperand();

            while (signStack.Count > 0)
                result = new AstNode<Tag>(signStack.Pop(), result);

            return result;
        }

        public static AstNode<Tag> ParsePowOperand(this IScanner<Token> scanner)
        {
            var operandStack = new Stack<AstNode<Tag>>();

            while (true)
            {
                operandStack.Push(scanner.ParseTerminal());

                if (!scanner.TryReadToken(Token.Caret))
                    break;
            }

            var result = operandStack.Pop();

            while (operandStack.Count > 0)
                result = new AstNode<Tag>(Tag.Power, operandStack.Pop(), result);

            return result;
        }

        public static AstNode<Tag> ParseTerminal(this IScanner<Token> scanner)
        {
            string text;
            if (scanner.TryReadToken(Token.Integer, out text))
                return new AstNode<Tag>(Tag.Integer, text);
            else if (scanner.TryReadToken(Token.Float, out text))
                return new AstNode<Tag>(Tag.Real, text);
            else if (scanner.TryReadToken(Token.String, out text))
                return new AstNode<Tag>(Tag.String, text);
            else if (scanner.TryReadToken(Token.LParen))
            {
                var expression = scanner.ParseExpression();
                scanner.ReadToken(Token.RParen);
                return expression;
            }
            else if (scanner.TryReadToken(Token.Identifier, out text))
            {
                IReadOnlyCollection<AstNode<Tag>> parameters = null;
                if (scanner.TryReadToken(Token.LBracket))
                {
                    parameters = scanner.ParseExpressions();
                    scanner.ReadToken(Token.RBracket);
                    return new AstNode<Tag>(Tag.Array, text, parameters);
                }
                else if (scanner.TryReadToken(Token.LParen))
                {
                    parameters = scanner.ParseExpressions();
                    scanner.ReadToken(Token.RParen);
                    return new AstNode<Tag>(Tag.Function, text, parameters);
                }
                else
                    return new AstNode<Tag>(Tag.Identifier, text);
            }
            else
                throw new ParserException(ErrorMessages.MissingLValue);
        }

        #endregion

        #region TryParse methods

        public static bool TryParseNextStatement(this IScanner<Token> scanner, out AstNode<Tag> result)
        {
            if (scanner.TryReadToken(Token.Next))
            {
                result = new AstNode<Tag>(Tag.Next);
                return true;
            }

            result = null;
            return false;
        }

        public static bool TryParseLetStatement(this IScanner<Token> scanner, out AstNode<Tag> result)
        {
            AstNode<Tag> lValue;
            AstNode<Tag> expression;

            if (scanner.TryReadToken(Token.Let))
            {
                lValue = scanner.ParseLValue();
                expression = scanner.ParseAssignment();
            }
            else if (scanner.TryParseLValue(out lValue))
            {
                expression = scanner.ParseAssignment();
            }
            else
            {
                result = null;
                return false;
            }

            result = new AstNode<Tag>(Tag.Let, lValue, expression);
            return true;
        }

        public static bool TryParseLValue(this IScanner<Token> scanner, out AstNode<Tag> result)
        {
            string identifier;
            if (scanner.TryReadToken(Token.Identifier, out identifier))
            {
                IReadOnlyCollection<AstNode<Tag>> parameters = null;
                if (scanner.TryReadToken(Token.LBracket))
                {
                    parameters = scanner.ParseExpressions();
                    scanner.ReadToken(Token.RBracket);
                    result = new AstNode<Tag>(Tag.Array, identifier, parameters);
                    return true;
                }

                result = new AstNode<Tag>(Tag.Identifier, identifier);
                return true;
            }

            result = null;
            return false;
        }

        public static AstNode<Tag> ParseAssignment(this IScanner<Token> scanner)
        {
            scanner.ReadToken(Token.Eq);
            return scanner.ParseExpression();
        }

        public static bool TryParseInputStatement(this IScanner<Token> scanner, out AstNode<Tag> result)
        {
            string input;
            if (scanner.TryReadToken(Token.Input))
            {
                if (scanner.TryReadToken(Token.String, out input))
                    scanner.ReadToken(Token.Comma);

                var lValue = scanner.ParseLValue();
                result = new AstNode<Tag>(Tag.Input, input, lValue);
                return true;
            }

            result = null;
            return false;
        }

        public static bool TryParsePrintStatement(this IScanner<Token> scanner, out AstNode<Tag> result)
        {
            if (scanner.TryReadToken(Token.Print))
            {
                var expressions = scanner.ParseExpressions();

                if (scanner.TryReadToken(Token.Semicolon))
                    result = new AstNode<Tag>(Tag.Print, expressions);
                else
                    result = new AstNode<Tag>(Tag.PrintLine, expressions);
                return true;
            }

            result = null;
            return false;
        }

        public static bool TryParseQuitStatement(this IScanner<Token> scanner, out AstNode<Tag> result)
        {
            if (scanner.TryReadToken(Token.Quit))
            {
                result = new AstNode<Tag>(Tag.Quit);
                return true;
            }

            result = null;
            return false;
        }

        #endregion
    }
}
