namespace LearningBasic
{
    using System;

    public static class BuiltInFunctions
    {
        public static void ThrowIfNotNumber(object operand, string exceptionMessage)
        {
            if (operand is int || operand is double)
                return;

            throw new ArithmeticException(exceptionMessage);
        }

        public static bool TryExecute<T1, T2>(object operand1, object operand2, Func<T1, T2, object> operation, out object result)
        {
            if (operand1 is T1 && operand2 is T2)
            {
                result = (object)operation((T1)operand1, (T2)operand2);
                return true;
            }

            result = null;
            return false;
        }

        public static object Execute<T1, T2>(object operand1, object operand2, Func<T1, T2, object> operation)
        {
            return operation((T1)operand1, (T2)operand2);
        }

        public static object Power(int @base, int exponent)
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

        public static object Power(double @base, int exponent)
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

        public static object Power(int @base, double exponent)
        {
            return Math.Pow(@base, exponent);
        }

        public static object Power(double @base, double exponent)
        {
            return Math.Pow(@base, exponent);
        }

        public static object Power(object @base, object exponent)
        {
            ThrowIfNotNumber(@base, "BASE has not NUMBER type");
            ThrowIfNotNumber(exponent, "EXPONENT has not NUMBER type");

            object result;
            if (TryExecute<int, int>(@base, exponent, Power, out result))
                return result;
            else if (TryExecute<double, int>(@base, exponent, Power, out result))
                return result;
            else if (TryExecute<int, double>(@base, exponent, Power, out result))
                return result;
            else
                return Execute<double, double>(@base, exponent, Power);
        }
    }
}
