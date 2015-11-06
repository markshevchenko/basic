namespace LearningBasic.Parsing.Ast.Conditions
{
    using System.Linq.Expressions;

    public class LessThanOrEqual : BinaryOperator
    {
        public LessThanOrEqual(IExpression left, IExpression right)
            : base(Associativity.Left, Priority.Comparison, "<=", left, right)
        { }

        protected override Expression BuildExpression(Expression left, Expression right)
        {
            return DynamicExpressionBuilder.BuildOperator(ExpressionType.LessThanOrEqual, left, right);
        }
    }
}
