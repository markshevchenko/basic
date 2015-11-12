namespace LearningBasic.Parsing.Code.Conditions
{
    using System.Linq.Expressions;

    public class LessThan : BinaryOperator
    {
        public LessThan(IExpression left, IExpression right)
            : base(Associativity.Left, Priority.Comparison, "<", left, right)
        { }

        protected override Expression BuildExpression(Expression left, Expression right)
        {
            return DynamicBuilder.BuildOperator(ExpressionType.LessThan, left, right);
        }
    }
}
