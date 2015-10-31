namespace LearningBasic.Parsing.Ast.Expressions
{
    using System.Linq.Expressions;

    public class Divide : BinaryOperator
    {
        public Divide(IExpression left, IExpression right)
            : base(Associativity.Left, Priority.ArithmeticMultiplication, "/", left, right)
        { }

        protected override Expression Calculate(Expression left, Expression right)
        {
            return PerformBuiltInOperator(ExpressionType.Divide, left, right);
        }
    }
}
