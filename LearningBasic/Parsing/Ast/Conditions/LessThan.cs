namespace LearningBasic.Parsing.Ast.Conditions
{
    using System.Linq.Expressions;

    public class LessThan : BinaryOperator
    {
        public LessThan(IExpression left, IExpression right)
            : base(Associativity.Left, Priority.Comparison, "<", left, right)
        { }

        protected override Expression BuildExpression(Expression left, Expression right)
        {
            return Expression.LessThan(left, right);
        }
    }
}
