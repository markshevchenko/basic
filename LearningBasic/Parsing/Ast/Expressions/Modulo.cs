namespace LearningBasic.Parsing.Ast.Expressions
{
    using System.Linq.Expressions;

    public class Modulo : BinaryOperator
    {
        public Modulo(IExpression left, IExpression right)
            : base(Associativity.Left, Priority.ArithmeticMultiplication, "%", left, right)
        { }

        protected override Expression Calculate(Expression left, Expression right)
        {
            return Calculate(ExpressionType.Modulo, left, right);
        }
    }
}
