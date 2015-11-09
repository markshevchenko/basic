namespace LearningBasic.Parsing.Code.Expressions
{
    using System.Linq.Expressions;

    public class Subtract : BinaryOperator
    {
        public Subtract(IExpression left, IExpression right)
            : base(Associativity.Left, Priority.ArithmeticAddition, "-", left, right)
        { }

        protected override Expression BuildExpression(Expression left, Expression right)
        {
            return DynamicExpressionBuilder.BuildOperator(ExpressionType.Subtract, left, right);
        }
    }
}
