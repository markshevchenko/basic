namespace LearningBasic.Parsing.Ast.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Microsoft.CSharp.RuntimeBinder;

    /// <summary>
    /// Defines the contract on n-ary expression that is for 1, 2-ary operators, and 0, 1, 2, 3-ary
    /// functions.
    /// </summary>
    public abstract class NaryExpression : IExpression
    {
        /// <summary>
        /// Gets the associativity of expression.
        /// </summary>
        public Associativity Associativity { get; private set; }

        /// <summary>
        /// Gets the priority of expression
        /// </summary>
        public Priority Priority { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NaryExpression"/> with specified
        /// associativity and priority.
        /// </summary>
        /// <param name="associativity">The associativity.</param>
        /// <param name="priority">The priority.</param>
        protected NaryExpression(Associativity associativity, Priority priority)
        {
            Associativity = associativity;
            Priority = priority;
        }

        /// <summary>
        /// Gets an <see cref="Expression"/> object of the n-ary expression.
        /// </summary>
        /// <param name="variables">The variables dictionary.</param>
        /// <returns>The expression.</returns>
        public abstract Expression GetExpression(IDictionary<string, dynamic> variables);

        /// <summary>
        /// Creates an <see cref="Expression"/> that represents a call to
        /// a overloaded static method that takes no arguments.
        /// </summary>
        /// <remarks>
        /// The argument types are used at run-time to select an overloaded static method. 
        /// </remarks>
        /// <param name="type">The type of the class containg the static method.</param>
        /// <param name="name">The name of the method.</param>
        /// <returns>An <see cref="Expression"/> that calls specified static method.</returns>
        public static Expression CallStaticMethod(Type type, string name)
        {
            var methodInfo = type.GetMethod(name, Type.EmptyTypes);
            return Expression.Call(methodInfo);
        }

        /// <summary>
        /// Creates an <see cref="Expression"/> that represents a call to
        /// an overloaded static method that takes one dynamic argument.
        /// </summary>
        /// <remarks>
        /// The argument types are used at run-time to select an overloaded static method. 
        /// </remarks>
        /// <param name="type">The type of the class containg the static method.</param>
        /// <param name="name">The name of the method.</param>
        /// <param name="arg1">The 1-st operand.</param>
        /// <returns>An <see cref="Expression"/> that calls specified static method.</returns>
        public static Expression CallStaticMethod(Type type, string name, Expression arg1)
        {
            var argumentInfo = new[]
            {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType
                                        | CSharpArgumentInfoFlags.IsStaticType, null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
            };

            var binder = Binder.InvokeMember(CSharpBinderFlags.None, name, null, typeof(NaryExpression), argumentInfo);

            var classExpression = Expression.Constant(type);

            return Expression.Dynamic(binder, typeof(object), classExpression, arg1);
        }

        /// <summary>
        /// Creates an <see cref="Expression"/> that represents a call to
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
        public static Expression CallStaticMethod(Type type, string name, Expression arg1, Expression art2)
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

            return Expression.Dynamic(binder, typeof(object), classExpression, arg1, art2);
        }

        /// <summary>
        /// Creates an <see cref="Expression"/> that represents a call to
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
        public static Expression CallStaticMethod(Type type, string name, Expression arg1, Expression arg2, Expression arg3)
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

            return Expression.Dynamic(binder, typeof(object), classExpression, arg1, arg2, arg3);
        }

        /// <summary>
        /// Creates an <see cref="Expression"/> that represents a performing
        /// built-in unary operator.
        /// </summary>
        /// <remarks>
        /// The argument types are used at run-time to select an overloaded operator.
        /// </remarks>
        /// <param name="type">The built-in unary operator, f.e. <see cref="ExpressionType.Negate"/>.</param>
        /// <param name="arg1">The 1-st operand.</param>
        /// <returns>An <see cref="Expression"/> that calls specified static method.</returns>
        public static Expression PerformBuiltInOperator(ExpressionType type, Expression arg1)
        {
            var argumentInfo = new[]
            {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
            };

            var binder = Binder.UnaryOperation(CSharpBinderFlags.None, type, typeof(NaryExpression), argumentInfo);

            return Expression.Dynamic(binder, typeof(object), arg1);
        }

        /// <summary>
        /// Creates an <see cref="Expression"/> that represents a performing
        /// built-in binary operator.
        /// </summary>
        /// <remarks>
        /// The argument types are used at run-time to select an overloaded operator.
        /// </remarks>
        /// <param name="type">The built-in binary operator, f.e. <see cref="ExpressionType.Add"/>.</param>
        /// <param name="arg1">The 1-st operand.</param>
        /// <param name="arg2">The 1-st operand.</param>
        /// <returns>An <see cref="Expression"/> that calls specified static method.</returns>
        public static Expression PerformBuiltInOperator(ExpressionType type, Expression arg1, Expression arg2)
        {
            var argumentInfo = new[]
            {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
            };

            var binder = Binder.BinaryOperation(CSharpBinderFlags.None, type, typeof(BinaryOperator), argumentInfo);

            return Expression.Dynamic(binder, typeof(object), arg1, arg2);
        }
    }
}
