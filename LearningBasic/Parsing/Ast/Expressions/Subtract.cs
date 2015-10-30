namespace LearningBasic.Parsing.Ast.Expressions
{
    using System.Linq.Expressions;

    public class Subtract : BinaryOperator
    {
        public Subtract(IExpression left, IExpression right)
            : base("-", left, right)
        { }

        protected override Expression Calculate(Expression left, Expression right)
        {
            return Calculate(ExpressionType.Subtract, left, right);
        }
    }
}
