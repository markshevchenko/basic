namespace LearningBasic.Parsing.Code.Conditions
{
    using System.Linq.Expressions;

    public class LessThanOrEqual : BinaryOperator
    {
        public LessThanOrEqual(IExpression left, IExpression right)
            : base(Associativity.Left, Priority.Comparison, "<=", left, right)
        { }

        protected override Expression BuildExpression(Expression left, Expression right)
        {
            return DynamicBuilder.BuildOperator(ExpressionType.LessThanOrEqual, left, right);
        }
    }
}
