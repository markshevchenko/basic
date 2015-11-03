namespace LearningBasic.Parsing.Ast.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
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

        public Expression GetExpression(IDictionary<string, dynamic> variables)
        {
            var left = Left.GetExpression(variables);
            var right = Right.GetExpression(variables);
            return Calculate(left, right);
        }

        public override string ToString()
        {
            var left = Left.ToString();
            var right = Right.ToString();

            if (Left.Priority < Priority || (Left.Priority == Priority && Associativity == Associativity.Right))
                left = '(' + left + ')';

            if (Right.Priority < Priority || (Right.Priority == Priority && Associativity == Associativity.Left))
                right = '(' + right + ')';

            return string.Format("{0} {1} {2}", left, Operator, right);
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
