namespace LearningBasic
{
    using System.Linq.Expressions;

    /// <summary>
    /// Helper object to provide testing of wring operatos like <see cref="ExpressionType.Assign"/>.
    /// </summary>
    public class WriteableExpressionVariable
    {
        /// <summary>
        /// The value that can be assigned inside an <see cref="Expression"/> tree.
        /// </summary>
        public object value;

        /// <summary>
        /// The expression that is "a reference to <see cref="value"/>",
        /// can be used to insert to an <see cref="Expression"/> tree.
        /// </summary>
        public readonly Expression expression;

        /// <summary>
        /// Initializes a new instance of <see cref="WriteableExpressionVariable"/> class with specified initial value.
        /// </summary>
        /// <param name="value">The initial value.</param>
        public WriteableExpressionVariable(object value)
        {
            this.value = value;
            var thisAsConstant = Expression.Constant(this);
            var fieldInfo = GetType().GetField("value");
            expression = Expression.Field(Expression.Constant(this), fieldInfo);
        }
    }
}
