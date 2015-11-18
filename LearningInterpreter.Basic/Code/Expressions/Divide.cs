namespace LearningInterpreter.Basic.Code.Expressions
{
    using System.Linq.Expressions;

    public class Divide : BinaryOperator
    {
        public Divide(IExpression left, IExpression right)
            : base(Associativity.Left, Priority.ArithmeticMultiplication, "/", left, right)
        { }

        protected override Expression BuildExpression(Expression left, Expression right)
        {
            return DynamicBuilder.BuildStaticCall(typeof(BuiltInOperators), "Divide", left, right);
        }
    }
}
