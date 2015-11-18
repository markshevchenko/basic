namespace LearningInterpreter.Basic.Code.Expressions
{
    using System.Linq.Expressions;

    public class Multiply : BinaryOperator
    {
        public Multiply(IExpression left, IExpression right)
            : base(Associativity.Left, Priority.ArithmeticMultiplication, "*", left, right)
        { }

        protected override Expression BuildExpression(Expression left, Expression right)
        {
            return DynamicBuilder.BuildOperator(ExpressionType.Multiply, left, right);
        }
    }
}
