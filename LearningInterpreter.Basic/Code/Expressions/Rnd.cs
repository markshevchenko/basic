namespace LearningInterpreter.Basic.Code.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using LearningInterpreter.RunTime;

    public class Rnd : Function
    {
        private static readonly MethodInfo NextDouble = typeof(Random).GetMethod("NextDouble");
        private static readonly MethodInfo Next1 = typeof(Random).GetMethod("Next", new[] { typeof(int) });
        private static readonly MethodInfo Next2 = typeof(Random).GetMethod("Next", new[] { typeof(int), typeof(int) });

        public Rnd(IReadOnlyList<IExpression> args)
            : base("RND", args)
        { }

        public override Expression GetExpression(Variables variables)
        {
            var random = Expression.Constant(variables.Random);
            var args = Args.Select(p => p.GetExpression(variables))
                           .ToArray();

            switch (args.Length)
            {
                case 0:
                    return Expression.Call(random, NextDouble);

                case 1:
                    return Expression.Call(random, Next1, args[0]);

                case 2:
                    return Expression.Call(random, Next2, args[0], args[1]);

                default:
                    var message = string.Format(ErrorMessages.TooManyArguments, Name, args.Length);
                    throw new InvalidOperationException(message);
            }
        }
    }
}
