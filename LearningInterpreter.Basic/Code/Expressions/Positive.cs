namespace LearningInterpreter.Basic.Code.Expressions
{
    using System.Linq.Expressions;

    public class Positive : UnaryOperator
    {
        public Positive(IExpression operand)
            : base(Associativity.Right, Priority.ArithmeticNegation, "+", operand)
        { }

        protected override Expression BuildExpression(Expression operand)
        {
            return DynamicBuilder.BuildStaticCall(typeof(BuiltInOperators), "Positive", operand);
        }
    }
}
