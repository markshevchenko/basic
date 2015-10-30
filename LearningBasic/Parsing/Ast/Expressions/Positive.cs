namespace LearningBasic.Parsing.Ast.Expressions
{
    using System;
    using System.Linq.Expressions;

    public class Positive : UnaryOperator
    {
        public Positive(IExpression operand)
            : base("+", operand)
        { }

        protected override Expression Calculate(Expression operand)
        {
            return Calculate(ExpressionType.UnaryPlus, operand);
        }
    }
}
