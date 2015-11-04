namespace LearningBasic.Parsing.Ast.Expressions
{
    using System;
    using System.Linq.Expressions;

    public class Negative : UnaryOperator
    {
        public Negative(IExpression operand)
            : base(Associativity.Right, Priority.ArithmeticNegation, "-", operand)
        { }

        protected override Expression Calculate(Expression operand)
        {
            return PerformBuiltInOperator(ExpressionType.Negate, operand);
        }
    }
}
