namespace LearningBasic.Parsing.Code.Expressions
{
    using System.Linq.Expressions;

    public class Power : BinaryOperator
    {
        public Power(IExpression left, IExpression right)
            : base(Associativity.Right, Priority.UpperIndex, "^", left, right)
        { }

        protected override Expression BuildExpression(Expression left, Expression right)
        {
            return DynamicBuilder.BuildStaticCall(typeof(BuiltInOperators), "Power", left, right);
        }
    }
}
