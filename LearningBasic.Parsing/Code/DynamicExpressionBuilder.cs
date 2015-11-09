namespace LearningBasic.Parsing.Code
{
    using System;
    using System.Linq.Expressions;
    using Microsoft.CSharp.RuntimeBinder;

    /// <summary>
    /// Implementes methods to build dynamic <see cref="Expression">expressions</see>.
    /// </summary>
    public static class DynamicExpressionBuilder
    {
        /// <summary>
        /// Builds an <see cref="Expression"/> that represents a call to
        /// a overloaded static method that takes no arguments.
        /// </summary>
        /// <remarks>
        /// The argument types are used at run-time to select an overloaded static method. 
        /// </remarks>
        /// <param name="type">The type of the class containg the static method.</param>
        /// <param name="name">The name of the method.</param>
        /// <returns>An <see cref="Expression"/> that calls specified static method.</returns>
        public static Expression BuildStaticCall(Type type, string name)
        {
            var methodInfo = type.GetMethod(name, Type.EmptyTypes);
            return Expression.Call(methodInfo);
        }

        /// <summary>
        /// Builds an <see cref="Expression"/> that represents a call to
        /// an overloaded static method that takes one dynamic argument.
        /// </summary>
        /// <remarks>
        /// The argument types are used at run-time to select an overloaded static method. 
        /// </remarks>
        /// <param name="type">The type of the class containg the static method.</param>
        /// <param name="name">The name of the method.</param>
        /// <param name="arg1">The 1-st operand.</param>
        /// <returns>An <see cref="Expression"/> that calls specified static method.</returns>
        public static Expression BuildStaticCall(Type type, string name, Expression arg1)
        {
            var argumentInfo = new[]
            {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType
                                        | CSharpArgumentInfoFlags.IsStaticType, null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
            };

            var binder = Binder.InvokeMember(CSharpBinderFlags.None, name, null, typeof(DynamicExpressionBuilder), argumentInfo);

            var classExpression = Expression.Constant(type);

            return Expression.Dynamic(binder, typeof(object), classExpression, arg1);
        }

        /// <summary>
        /// Builds an <see cref="Expression"/> that represents a call to
        /// an overloaded static method that takes two dynamic arguments.
        /// </summary>
        /// <remarks>
        /// The argument types are used at run-time to select an overloaded static method. 
        /// </remarks>
        /// <param name="type">The type of the class containg the static method.</param>
        /// <param name="name">The name of the method.</param>
        /// <param name="arg1">The 1-st operand.</param>
        /// <param name="arg2">The 2-nd operand.</param>
        /// <returns>An <see cref="Expression"/> that calls specified static method.</returns>
        public static Expression BuildStaticCall(Type type, string name, Expression arg1, Expression art2)
        {
            var argumentInfo = new[]
            {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType
                                        | CSharpArgumentInfoFlags.IsStaticType, null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
            };

            var binder = Binder.InvokeMember(CSharpBinderFlags.None, name, null, typeof(DynamicExpressionBuilder), argumentInfo);

            var classExpression = Expression.Constant(type);

            return Expression.Dynamic(binder, typeof(object), classExpression, arg1, art2);
        }

        /// <summary>
        /// Builds an <see cref="Expression"/> that represents a call to
        /// an overloaded static method that takes three dynamic arguments.
        /// </summary>
        /// <remarks>
        /// The argument types are used at run-time to select an overloaded static method. 
        /// </remarks>
        /// <param name="type">The type of the class containg the static method.</param>
        /// <param name="name">The name of the method.</param>
        /// <param name="arg1">The 1-st operand.</param>
        /// <param name="arg2">The 2-nd operand.</param>
        /// <param name="arg3">The 3-rd operand.</param>
        /// <returns>An <see cref="Expression"/> that calls specified static method.</returns>
        public static Expression BuildStaticCall(Type type, string name, Expression arg1, Expression arg2, Expression arg3)
        {
            var argumentInfo = new[]
            {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType
                                        | CSharpArgumentInfoFlags.IsStaticType, null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
            };

            var binder = Binder.InvokeMember(CSharpBinderFlags.None, name, null, typeof(DynamicExpressionBuilder), argumentInfo);

            var classExpression = Expression.Constant(type);

            return Expression.Dynamic(binder, typeof(object), classExpression, arg1, arg2, arg3);
        }

        /// <summary>
        /// Builds an <see cref="Expression"/> that represents a performing
        /// built-in unary operator.
        /// </summary>
        /// <remarks>
        /// The argument types are used at run-time to select an overloaded operator.
        /// </remarks>
        /// <param name="type">The built-in unary operator, f.e. <see cref="ExpressionType.Negate"/>.</param>
        /// <param name="arg1">The 1-st operand.</param>
        /// <returns>An <see cref="Expression"/> that calls specified static method.</returns>
        public static Expression BuildOperator(ExpressionType type, Expression arg1)
        {
            var argumentInfo = new[]
            {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
            };

            var binder = Binder.UnaryOperation(CSharpBinderFlags.None, type, typeof(DynamicExpressionBuilder), argumentInfo);

            return Expression.Dynamic(binder, typeof(object), arg1);
        }

        /// <summary>
        /// Builds an <see cref="Expression"/> that represents a performing
        /// built-in binary operator.
        /// </summary>
        /// <remarks>
        /// The argument types are used at run-time to select an overloaded operator.
        /// </remarks>
        /// <param name="type">The built-in binary operator, f.e. <see cref="ExpressionType.Add"/>.</param>
        /// <param name="arg1">The 1-st operand.</param>
        /// <param name="arg2">The 1-st operand.</param>
        /// <returns>An <see cref="Expression"/> that calls specified static method.</returns>
        public static Expression BuildOperator(ExpressionType type, Expression arg1, Expression arg2)
        {
            var argumentInfo = new[]
            {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
            };

            var binder = Binder.BinaryOperation(CSharpBinderFlags.None, type, typeof(DynamicExpressionBuilder), argumentInfo);

            return Expression.Dynamic(binder, typeof(object), arg1, arg2);
        }

        public static Expression BuildLogicalAnd(Expression arg1, Expression arg2)
        {
            var argumentInfo = new[]
            {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
            };

            var binder = Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof(DynamicExpressionBuilder), argumentInfo);

            return Expression.Dynamic(binder, typeof(object), arg1, arg2);
        }

        public static Expression BuildLogicalOr(Expression arg1, Expression arg2)
        {
            var argumentInfo = new[]
            {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
            };

            var binder = Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.Or, typeof(DynamicExpressionBuilder), argumentInfo);

            return Expression.Dynamic(binder, typeof(object), arg1, arg2);
        }

        public static Expression BuildConvert(Expression arg, Type type)
        {
            var binder = Binder.Convert(CSharpBinderFlags.None, type, typeof(DynamicExpressionBuilder));

            return Expression.Dynamic(binder, type, arg);
        }
    }
}
