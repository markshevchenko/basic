namespace LearningBasic.Parsing.Ast.Expressions
{
    using System.Linq.Expressions;

    public class Add : BinaryOperator
    {
        public Add(IExpression left, IExpression right)
            : base(Associativity.Left, Priority.ArithmeticAddition, "+", left, right)
        { }

        protected override Expression Calculate(Expression left, Expression right)
        {
            return Calculate(ExpressionType.Add, left, right);
        }
    }
}
