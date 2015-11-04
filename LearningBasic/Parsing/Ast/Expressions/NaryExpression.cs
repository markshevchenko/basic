namespace LearningBasic.Parsing.Ast.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Microsoft.CSharp.RuntimeBinder;

    public abstract class NaryExpression : IExpression
    {
        public Associativity Associativity { get; private set; }

        public Priority Priority { get; private set; }

        public NaryExpression(Associativity associativity, Priority priority)
        {
            Associativity = associativity;
            Priority = priority;
        }

        public abstract Expression GetExpression(IDictionary<string, dynamic> variables);

        public static Expression CallStaticMethod(Type type, string name)
        {
            var argumentInfo = new[]
            {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType
                                        | CSharpArgumentInfoFlags.IsStaticType, null),
            };

            var binder = Binder.InvokeMember(CSharpBinderFlags.None, name, null, typeof(NaryExpression), argumentInfo);

            var classExpression = Expression.Constant(type);

            return Expression.Dynamic(binder, typeof(object), classExpression);
        }

        public static Expression CallStaticMethod(Type type, string name, Expression o1)
        {
            var argumentInfo = new[]
            {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType
                                        | CSharpArgumentInfoFlags.IsStaticType, null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
            };

            var binder = Binder.InvokeMember(CSharpBinderFlags.None, name, null, typeof(NaryExpression), argumentInfo);

            var classExpression = Expression.Constant(type);

            return Expression.Dynamic(binder, typeof(object), classExpression, o1);
        }

        public static Expression CallStaticMethod(Type type, string name, Expression o1, Expression o2)
        {
            var argumentInfo = new[]
            {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType
                                        | CSharpArgumentInfoFlags.IsStaticType, null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
            };

            var binder = Binder.InvokeMember(CSharpBinderFlags.None, name, null, typeof(NaryExpression), argumentInfo);

            var classExpression = Expression.Constant(type);

            return Expression.Dynamic(binder, typeof(object), classExpression, o1, o2);
        }

        public static Expression CallStaticMethod(Type type, string name, Expression o1, Expression o2, Expression o3)
        {
            var argumentInfo = new[]
            {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType
                                        | CSharpArgumentInfoFlags.IsStaticType, null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
            };

            var binder = Binder.InvokeMember(CSharpBinderFlags.None, name, null, typeof(NaryExpression), argumentInfo);

            var classExpression = Expression.Constant(type);

            return Expression.Dynamic(binder, typeof(object), classExpression, o1, o2, o3);
        }

        public static Expression PerformBuiltInOperator(ExpressionType type, Expression o1)
        {
            var argumentInfo = new[]
            {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
            };

            var binder = Binder.UnaryOperation(CSharpBinderFlags.None, type, typeof(NaryExpression), argumentInfo);

            return Expression.Dynamic(binder, typeof(object), o1);
        }

        public static Expression PerformBuiltInOperator(ExpressionType type, Expression o1, Expression o2)
        {
            var argumentInfo = new[]
            {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
            };

            var binder = Binder.BinaryOperation(CSharpBinderFlags.None, type, typeof(BinaryOperator), argumentInfo);

            return Expression.Dynamic(binder, typeof(object), o1, o2);
        }
    }
}
