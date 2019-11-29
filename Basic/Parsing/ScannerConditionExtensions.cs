﻿namespace Basic.Parsing
{
    using Basic.Runtime;
    using Basic.Runtime.Conditions;

    /// <summary>
    /// Implements extension methods to parse BASIC conditions (comparisons and logical expressions).
    /// </summary>
    public static class ScannerConditionExtensions
    {
        public static IExpression ReadCondition(this Scanner scanner)
        {
            var result = scanner.ReadOrOperand();

            while (true)
            {
                IExpression tail;
                if (scanner.TryReadToken(Token.Or))
                {
                    tail = scanner.ReadOrOperand();
                    result = new Or(result, tail);
                }
                else if (scanner.TryReadToken(Token.Xor))
                {
                    tail = scanner.ReadOrOperand();
                    result = new Xor(result, tail);
                }
                else
                    return result;
            }
        }

        public static IExpression ReadOrOperand(this Scanner scanner)
        {
            var result = scanner.ReadAndOperand();

            while (true)
            {
                IExpression tail;
                if (scanner.TryReadToken(Token.And))
                {
                    tail = scanner.ReadAndOperand();
                    result = new And(result, tail);
                }
                else
                    return result;
            }
        }

        public static IExpression ReadAndOperand(this Scanner scanner)
        {
            if (scanner.TryReadToken(Token.Not))
                return new Not(scanner.ReadAndOperand());
            if (scanner.TryReadToken(Token.LParen))
            {
                var result = scanner.ReadCondition();
                scanner.ReadToken(Token.RParen);
                return result;
            }
            else
                return scanner.ReadRelation();
        }

        public static IExpression ReadRelation(this Scanner scanner)
        {
            var left = scanner.ReadExpression();

            if (scanner.TryReadToken(Token.Eq))
            {
                var right = scanner.ReadExpression();
                return new Equal(left, right);
            }
            else if (scanner.TryReadToken(Token.Ne))
            {
                var right = scanner.ReadExpression();
                return new NotEqual(left, right);
            }
            else if (scanner.TryReadToken(Token.Gt))
            {
                var right = scanner.ReadExpression();
                return new GreaterThan(left, right);
            }
            else if (scanner.TryReadToken(Token.Ge))
            {
                var right = scanner.ReadExpression();
                return new GreaterThanOrEqual(left, right);
            }
            else if (scanner.TryReadToken(Token.Lt))
            {
                var right = scanner.ReadExpression();
                return new LessThan(left, right);
            }
            else if (scanner.TryReadToken(Token.Le))
            {
                var right = scanner.ReadExpression();
                return new LessThanOrEqual(left, right);
            }
            else
                throw new ParserException(ErrorMessages.MissingRelationOperator);
        }
    }
}
