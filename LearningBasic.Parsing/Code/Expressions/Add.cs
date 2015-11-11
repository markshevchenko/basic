namespace LearningBasic.Parsing.Code.Expressions
{
    using System.Linq.Expressions;

    public class Add : BinaryOperator
    {
        public Add(IExpression left, IExpression right)
            : base(Associativity.Left, Priority.ArithmeticAddition, "+", left, right)
        { }

        protected override Expression BuildExpression(Expression left, Expression right)
        {
            return DynamicBuilder.BuildOperator(ExpressionType.Add, left, right);
        }
    }
}
