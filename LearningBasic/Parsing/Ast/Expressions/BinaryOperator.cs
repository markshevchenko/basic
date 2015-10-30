namespace LearningBasic.Parsing.Ast.Expressions
{
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using Microsoft.CSharp.RuntimeBinder;

    public abstract class BinaryOperator : IExpression
    {
        public string Operator { get; set; }

        public IExpression Left { get; set; }

        public IExpression Right { get; set; }

        protected BinaryOperator(string @operator, IExpression left, IExpression right)
        {
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
