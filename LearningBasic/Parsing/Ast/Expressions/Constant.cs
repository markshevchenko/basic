namespace LearningBasic.Parsing.Ast.Expressions
{
    using System;
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

        public Expression Compile(IRunTimeEnvironment rte)
        {
            return compiledValue;
        }

        public override string ToString()
        {
            if (Value == null)
                return string.Empty;

            if (Value is string)
                return '"' + (string)Value + '"';

            if (Value is IConvertible)
                return (Value as IConvertible).ToString(CultureInfo.InvariantCulture);

            return Value.ToString();
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
