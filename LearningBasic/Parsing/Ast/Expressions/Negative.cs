namespace LearningBasic.Parsing.Ast.Expressions
{
    using System;
    using System.Linq.Expressions;

    public class Negative : UnaryOperator
    {
        public Negative(IExpression operand)
            : base(Associativity.Right, Priority.ArithmeticNegation, "-", operand)
        { }

        protected override Expression BuildExpression(Expression operand)
        {
            return DynamicExpressionBuilder.BuildOperator(ExpressionType.Negate, operand);
        }
    }
}
