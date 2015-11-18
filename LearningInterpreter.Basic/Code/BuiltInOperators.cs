namespace LearningInterpreter.Basic.Code
{
    using Microsoft.CSharp.RuntimeBinder;
    using System;
    using System.Globalization;

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

        public static dynamic Divide(double numerator, double denominator)
        {
            return numerator / denominator;
        }

        public static dynamic Positive(int value)
        {
            return value;
        }

        public static dynamic Positive(double value)
        {
            return value;
        }

        public static dynamic Positive(string value)
        {
            int i;
            if (int.TryParse(value, NumberStyles.None, CultureInfo.InvariantCulture, out i))
                return i;

            double d;
            if (double.TryParse(value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out d))
                return d;

            throw new RuntimeBinderException();
        }
    }
}
