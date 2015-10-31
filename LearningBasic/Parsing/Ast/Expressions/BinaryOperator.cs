namespace LearningBasic.Parsing.Ast.Expressions
{
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using Microsoft.CSharp.RuntimeBinder;
    using System;

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

        public static Expression CallStaticMethod(Type methodClass, string methodName, Expression left, Expression right)
        {
            var argumentInfo = new[]
            {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
            };

            var binder = Binder.InvokeMember(CSharpBinderFlags.None, methodName, null, typeof(BinaryOperator), argumentInfo);

            var classExpression = Expression.Constant(methodClass);

            return Expression.Dynamic(binder, typeof(object), classExpression, left, right);
        }

        public static Expression PerformBuiltInOperator(ExpressionType @operator, Expression left, Expression right)
        {
            var argumentInfo = new[]
            {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
            };

            var binder = Binder.BinaryOperation(CSharpBinderFlags.None, @operator, typeof(BinaryOperator), argumentInfo);

            return Expression.Dynamic(binder, typeof(object), left, right);
        }
    }
}
