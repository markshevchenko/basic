namespace LearningBasic.Parsing.Ast.Conditions
{
    using System.Linq.Expressions;

    public class NotEqual : BinaryOperator
    {
        public NotEqual(IExpression left, IExpression right)
            : base(Associativity.Left, Priority.Comparison, "<>", left, right)
        { }

        protected override Expression BuildExpression(Expression left, Expression right)
        {
            return PerformBuiltInOperator(ExpressionType.NotEqual, left, right);
        }
    }
}
