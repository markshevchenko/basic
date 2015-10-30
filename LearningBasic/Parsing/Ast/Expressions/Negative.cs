namespace LearningBasic.Parsing.Ast.Expressions
{
    using System;
    using System.Linq.Expressions;

    public class Negative : UnaryOperator
    {
        public Negative(IExpression operand)
            : base("-", operand)
        { }

        protected override Expression Calculate(Expression operand)
        {
            return Calculate(ExpressionType.Negate, operand);
        }
    }
}
