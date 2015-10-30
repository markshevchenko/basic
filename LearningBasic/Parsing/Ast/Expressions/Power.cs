namespace LearningBasic.Parsing.Ast.Expressions
{
    using System.Linq.Expressions;
    using System.Reflection;

    public class Power : BinaryOperator
    {
        private static readonly MethodInfo PowerMethodInfo = typeof(BuiltInFunctions).GetMethod("Power", new[] { typeof(object), typeof(object) });

        public Power(IExpression left, IExpression right)
            : base(Associativity.Right, Priority.UpperIndex, "^", left, right)
        { }

        protected override Expression Calculate(Expression left, Expression right)
        {
            return Expression.Call(PowerMethodInfo, left, right);
        }
    }
}
