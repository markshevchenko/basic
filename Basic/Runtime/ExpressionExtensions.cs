namespace Basic.Runtime
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Implements extension methods for <see cref="Expression"/> trees.
    /// </summary>
    public static class ExpressionExtensions
    {
        private static readonly MethodInfo ToStringWithFormatProvider = typeof(IConvertible).GetMethod("ToString", new[] { typeof(IFormatProvider) });

        /// <summary>
        /// Calculates the value of the expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The value of expression converted (boxed) to <see cref="System.Object">object</see> type.</returns>
        public static object Calculate(this Expression expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            if (expression.Type != typeof(object))
                expression = Expression.Convert(expression, typeof(object));

            var compiledExpression = Expression.Lambda<Func<object>>(expression)
                                               .Compile();

            return compiledExpression();
        }

        /// <summary>
        /// Runs an expression with side-effects like <see cref="Expression.Assign(Expression, Expression)">assignment</see>,
        /// and drops the result value.
        /// </summary>
        /// <param name="expression">The expression with side effects.</param>
        public static void RunAndDropValue(this Expression expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            var compiledExpression = Expression.Lambda<Action>(expression)
                                               .Compile();

            compiledExpression();
        }

        /// <summary>
        /// Converts the value of the expression to an equivalent <see cref="String"/>
        /// using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="formatProvider">
        /// An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific
        /// formatting information.
        /// </param>
        /// <returns>The <see cref="String"/> instance equivalent to the value of the expression.</returns>
        public static string ToString(this Expression expression, IFormatProvider formatProvider)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            if (formatProvider == null)
                throw new ArgumentNullException(nameof(formatProvider));

            var expressionAsConvertible = Expression.Convert(expression, typeof(IConvertible));
            var formatProviderExpression = Expression.Constant(formatProvider);
            var expressionAsString = Expression.Call(expressionAsConvertible, ToStringWithFormatProvider, formatProviderExpression);
            var getValue = Expression.Lambda<Func<string>>(expressionAsString)
                                     .Compile();
            return getValue();
        }
    }
}
