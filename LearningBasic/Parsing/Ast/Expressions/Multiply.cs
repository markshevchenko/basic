namespace LearningBasic.Parsing.Ast.Expressions
{
    using System.Linq.Expressions;

    public class Multiply : BinaryOperator
    {
        public Multiply(IExpression left, IExpression right)
            : base(Associativity.Left, Priority.ArithmeticMultiplication, "*", left, right)
        { }

        protected override Expression Calculate(Expression left, Expression right)
        {
            return PerformBuiltInOperator(ExpressionType.Multiply, left, right);
        }
    }
}
