namespace LearningBasic.Parsing.Ast
{
    using System;

    /// <summary>
    /// Implements specific BASIC operators those actions differs from C#.
    /// </summary>
    public static class BuiltInOperators
    {
        public static dynamic Power(int @base, int exponent)
        {
            if (@base == 0 && exponent <= 0)
                throw new DivideByZeroException();

            var sign = Math.Sign(exponent);
            exponent = Math.Abs(exponent);
            var result = 1;

            for (int i = 0; i < exponent; i++)
                result *= @base;

            if (sign >= 0)
                return result;

            return 1.0 / result;
        }

        public static dynamic Power(double @base, int exponent)
        {
            if (@base == 0.0 && exponent <= 0)
                throw new DivideByZeroException();

            var sign = Math.Sign(exponent);
            exponent = Math.Abs(exponent);
            var result = 1.0;

            for (int i = 0; i < exponent; i++)
                result *= @base;

            if (sign >= 0)
                return result;

            return 1.0 / result;
        }

        public static dynamic Power(int @base, double exponent)
        {
            return Math.Pow(@base, exponent);
        }

        public static dynamic Power(double @base, double exponent)
        {
            return Math.Pow(@base, exponent);
        }

        public static dynamic Divide(int numerator, int denominator)
        {
            if (numerator % denominator == 0)
                return numerator / denominator;

            return (double)numerator / denominator;
        }

        public static dynamic Divide(int numerator, double denominator)
        {
            return numerator / denominator;
        }

        public static dynamic Divide(double numerator, int denominator)
        {
            return numerator / denominator;
        }

        public static dynamic Divide(double numerator, double denominator)
        {
            return numerator / denominator;
        }
    }
}
