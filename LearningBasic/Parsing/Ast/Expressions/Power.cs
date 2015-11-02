namespace LearningBasic.Parsing.Ast.Expressions
{
    using System.Linq.Expressions;

    public class Power : BinaryOperator
    {
        public Power(IExpression left, IExpression right)
            : base(Associativity.Right, Priority.UpperIndex, "^", left, right)
        { }

        protected override Expression Calculate(Expression left, Expression right)
        {
            return CallStaticMethod(typeof(BuiltInOperators), "Power", left, right);
        }
    }
}
