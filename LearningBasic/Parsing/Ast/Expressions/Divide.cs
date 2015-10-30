namespace LearningBasic.Parsing.Ast.Expressions
{
    using System.Linq.Expressions;

    public class Divide : BinaryOperator
    {
        public Divide(IExpression left, IExpression right)
            : base("/", left, right)
        { }

        protected override Expression Calculate(Expression left, Expression right)
        {
            return Calculate(ExpressionType.Divide, left, right);
        }
    }
}
