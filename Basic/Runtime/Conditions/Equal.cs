namespace Basic.Runtime.Conditions
{
    using System.Linq.Expressions;

    public class Equal : BinaryOperator
    {
        public Equal(IExpression left, IExpression right)
            : base(Associativity.Left, Priority.Comparison, "=", left, right)
        { }

        protected override Expression BuildExpression(Expression left, Expression right)
        {
            return DynamicBuilder.BuildOperator(ExpressionType.Equal, left, right);
        }
    }
}
