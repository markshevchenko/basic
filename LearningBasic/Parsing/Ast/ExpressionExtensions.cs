namespace LearningBasic.Parsing.Ast
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class ExpressionExtensions
    {
        private static readonly MethodInfo ToStringWithFormatProvider = typeof(IConvertible).GetMethod("ToString", new[] { typeof(IFormatProvider) });

        public static object CalculateValue(this Expression expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            var compiledExpression = Expression.Lambda<Func<object>>(expression)
                                               .Compile();
            return compiledExpression();
        }

        public static string ToString(this Expression expression, IFormatProvider formatProvider)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            if (formatProvider == null)
                throw new ArgumentNullException("formatProvider");

            var expressionAsConvertible = Expression.Convert(expression, typeof(IConvertible));
            var formatProviderExpression = Expression.Constant(formatProvider);
            var expressionAsString = Expression.Call(expressionAsConvertible, ToStringWithFormatProvider, formatProviderExpression);
            var getValue = Expression.Lambda<Func<string>>(expressionAsString)
                                     .Compile();
            return getValue();
        }
    }
}
