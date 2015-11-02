namespace LearningBasic.Parsing.Ast.Expressions
{
    using System.Linq.Expressions;

    public class Subtract : BinaryOperator
    {
        public Subtract(IExpression left, IExpression right)
            : base(Associativity.Left, Priority.ArithmeticAddition, "-", left, right)
        { }

        protected override Expression Calculate(Expression left, Expression right)
        {
            return PerformBuiltInOperator(ExpressionType.Subtract, left, right);
        }
    }
}
