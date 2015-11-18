namespace LearningInterpreter.Basic.Code.Conditions
{
    using System.Linq.Expressions;

    public class GreaterThanOrEqual : BinaryOperator
    {
        public GreaterThanOrEqual(IExpression left, IExpression right)
            : base(Associativity.Left, Priority.Comparison, ">=", left, right)
        { }

        protected override Expression BuildExpression(Expression left, Expression right)
        {
            return DynamicBuilder.BuildOperator(ExpressionType.GreaterThanOrEqual, left, right);
        }
    }
}
