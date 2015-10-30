namespace LearningBasic.Parsing.Ast.Expressions
{
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using Microsoft.CSharp.RuntimeBinder;

    public abstract class UnaryOperator : IExpression
    {
        public Associativity Associativity { get; private set; }

        public Priority Priority { get; private set; }

        public string Operator { get; private set; }

        public IExpression Operand { get; private set; }

        protected UnaryOperator(Associativity associativity, Priority priority, string @operator, IExpression operand)
        {
            Associativity = associativity;
            Priority = priority;
            Operator = @operator;
            Operand = operand;
        }

        protected abstract Expression Calculate(Expression operand);

        public Expression Compile(IRunTimeEnvironment rte)
        {
            var operand = Operand.Compile(rte);
            return Calculate(operand);
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", Operator, Operand);
        }

        public static Expression Calculate(ExpressionType expressionType, Expression operand)
        {
            var binder = CreateBinder(expressionType);
            return Expression.Dynamic(binder, typeof(object), operand);
        }

        public static CallSiteBinder CreateBinder(ExpressionType expressionType)
        {
            var operands = new[]
            {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, "operand"),
            };

            return Binder.UnaryOperation(CSharpBinderFlags.None, expressionType, typeof(UnaryOperator), operands);
        }
    }
}
