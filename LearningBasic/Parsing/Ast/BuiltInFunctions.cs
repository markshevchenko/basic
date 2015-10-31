namespace LearningBasic.Parsing.Ast
{
    using System;

    public static class BuiltInFunctions
    {
        public static dynamic Power(int @base, int exponent)
        {
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
    }
}
