namespace LearningBasic.Parsing.Ast.Expressions
{
    using System.Linq.Expressions;

    public class Add : BinaryOperator
    {
        public Add(IExpression left, IExpression right)
            : base(Associativity.Left, Priority.ArithmeticAddition, "+", left, right)
        { }

        protected override Expression BuildExpression(Expression left, Expression right)
        {
            return DynamicExpressionBuilder.BuildOperator(ExpressionType.Add, left, right);
        }
    }
}
