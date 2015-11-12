namespace LearningBasic.Parsing.Code.Conditions
{
    using System.Linq.Expressions;

    public class Not : UnaryOperator
    {
        public Not(IExpression operand)
            : base(Associativity.Right, Priority.LogicalNegation, "NOT", operand)
        { }

        protected override Expression BuildExpression(Expression operand)
        {
            return DynamicBuilder.BuildOperator(ExpressionType.Not, operand);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Operator + " " + OperandAsString;
        }
    }
}
