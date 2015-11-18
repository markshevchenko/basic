namespace LearningInterpreter.Basic.Code.Expressions
{
    using System;
    using System.Globalization;
    using System.Linq.Expressions;
    using LearningInterpreter.RunTime;

    public class Constant : IExpression
    {
        private readonly Expression compiledValue;

        public object Value { get; }

        public Associativity Associativity { get { return Associativity.Left; } }

        public Priority Priority { get { return Priority.Terminal; } }

        public Constant(string value)
        {
            Value = Parse(value);
            compiledValue = Expression.Constant(Value);
        }

        public Constant(object value)
        {
            Value = value;
            compiledValue = Expression.Constant(Value);
        }

        public virtual Expression GetExpression(Variables variables)
        {
            return compiledValue;
        }

        public override string ToString()
        {
            return ToString(Value);
        }

        public static string ToString(object value)
        {
            if (value == null)
                return string.Empty;

            if (value is string)
                return '"' + (value as string).Replace("\"", "\"\"") + '"';

            if (value is IConvertible)
                return (value as IConvertible).ToString(CultureInfo.InvariantCulture);

            return value.ToString();
        }

        public static object Parse(string s)
        {
            int i;
            if (int.TryParse(s, NumberStyles.None, CultureInfo.InvariantCulture, out i))
                return i;

            double d;
            if (double.TryParse(s, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out d))
                return d;

            return s;
        }
    }
}
