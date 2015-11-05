namespace LearningBasic.Parsing.Ast.Conditions
{
    using System.Linq.Expressions;

    public class Xor : BinaryOperator
    {
        public Xor(IExpression left, IExpression right)
            : base(Associativity.Left, Priority.LogicalMultiplication, "XOR", left, right)
        { }

        protected override Expression BuildExpression(Expression left, Expression right)
        {
            return PerformBuiltInOperator(ExpressionType.ExclusiveOr, left, right);
        }
    }
}
