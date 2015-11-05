namespace LearningBasic.Parsing.Ast.Conditions
{
    using System.Linq.Expressions;

    public class Not : UnaryOperator
    {
        public Not(IExpression operand)
            : base(Associativity.Right, Priority.LogicalNegation, "NOT", operand)
        {
            DoInsertSpacebar = true;
        }

        protected override Expression BuildExpression(Expression operand)
        {
            return PerformBuiltInOperator(ExpressionType.Not, operand);
        }
    }
}
