namespace LearningBasic.Parsing.Ast.Expressions
{
    using System.Globalization;
    using System.Linq.Expressions;

    public class Constant : IExpression
    {
        private readonly Expression compiledValue;

        public object Value { get; }

        public Constant(string value)
        {
            Value = Parse(value);
            compiledValue = Expression.Convert(Expression.Constant(value), typeof(object));
        }

        public Expression Compile(IRunTimeEnvironment rte)
        {
            return compiledValue;
        }

        public static object Parse(string s)
        {
            int i;
            if (int.TryParse(s, NumberStyles.None, CultureInfo.InvariantCulture, out i))
                return i;

            double d;
            if (double.TryParse(s, NumberStyles.None, CultureInfo.InvariantCulture, out d))
                return d;

            return s;
        }
    }
}
