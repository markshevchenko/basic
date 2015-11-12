namespace LearningBasic.Parsing.Code.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq.Expressions;

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

        public virtual Expression GetExpression(IDictionary<string, dynamic> variables)
        {
            return compiledValue;
        }

        public override string ToString()
        {
            if (Value == null)
                return string.Empty;

            if (Value is string)
                return Quote(Value as string);

            if (Value is IConvertible)
                return (Value as IConvertible).ToString(CultureInfo.InvariantCulture);

            return Value.ToString();
        }

        public static string Quote(string s)
        {
            return '"' + s.Replace("\"", "\"\"") + '"';
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
