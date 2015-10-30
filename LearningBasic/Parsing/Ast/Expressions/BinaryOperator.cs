namespace LearningBasic.Parsing.Ast.Expressions
{
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using Microsoft.CSharp.RuntimeBinder;

    public abstract class BinaryOperator : IExpression
    {
        public Associativity Associativity { get; private set; }

        public Priority Priority { get; private set; }

        public string Operator { get; private set; }

        public IExpression Left { get; private set; }

        public IExpression Right { get; private set; }

        protected BinaryOperator(Associativity associativity, Priority priority, string @operator, IExpression left, IExpression right)
        {
            Associativity = associativity;
            Priority = priority;
            Operator = @operator;
            Left = left;
            Right = right;
        }

        protected abstract Expression Calculate(Expression left, Expression right);

        public Expression Compile(IRunTimeEnvironment rte)
        {
            var left = Left.Compile(rte);
            var right = Right.Compile(rte);
            return Calculate(left, right);
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", Left, Operator, Right);
        }

        public static Expression Calculate(ExpressionType expressionType, Expression left, Expression right)
        {
            var binder = CreateBinder(expressionType);
            return Expression.Dynamic(binder, typeof(object), left, right);
        }

        public static CallSiteBinder CreateBinder(ExpressionType expressionType)
        {
            var operands = new[]
            {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, "left"),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, "right"),
            };

            return Binder.BinaryOperation(CSharpBinderFlags.None, expressionType, typeof(UnaryOperator), operands);
        }
    }
}
