namespace LearningBasic
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using LearningBasic.Evaluating;
    using Microsoft.CSharp.RuntimeBinder;

    public static class ExpressionExtensions
    {
        private static readonly MethodInfo ToStringWithFormatProvider = typeof(IConvertible).GetMethod("ToString", new[] { typeof(IFormatProvider) });

        public static object CalculateValue(this Expression expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            try
            {
                if (expression.Type != typeof(object))
                    expression = Expression.Convert(expression, typeof(object));

                var compiledExpression = Expression.Lambda<Func<object>>(expression)
                                                   .Compile();

                return compiledExpression();
            }
            catch (RuntimeBinderException exception)
            {
                throw new RunTimeException(ErrorMessages.InvalidParameters, exception);
            }
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
