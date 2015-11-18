namespace LearningInterpreter.Basic.Code.Conditions
{
    using System.Linq.Expressions;

    public class Or : BinaryOperator
    {
        public Or(IExpression left, IExpression right)
            : base(Associativity.Left, Priority.LogicalMultiplication, "OR", left, right)
        { }

        protected override Expression BuildExpression(Expression left, Expression right)
        {
            return DynamicBuilder.BuildLogicalOr(left, right);
        }
    }
}
