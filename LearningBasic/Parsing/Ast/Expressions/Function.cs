namespace LearningBasic.Parsing.Ast.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class Function : IExpression
    {
        public Associativity Associativity { get { return Associativity.Left; } }

        public Priority Priority { get { return Priority.LowerIndex; } }

        public string Name { get; private set; }

        public IReadOnlyList<IExpression> Args { get; private set; }

        public Function(string name, params IExpression[] args)
        {
            Name = name.ToUpper();
            Args = args;
        }

        public Function(string name, IReadOnlyList<IExpression> args)
        {
            Name = name.ToUpper();
            Args = args;
        }

        public virtual Expression GetExpression(IDictionary<string, dynamic> variables)
        {
            var args = Args.Select(p => p.GetExpression(variables))
                           .ToArray();

            switch (args.Length)
            {
                case 0:
                    return DynamicExpressionBuilder.BuildStaticCall(typeof(BuiltInFunctions), Name);

                case 1:
                    return DynamicExpressionBuilder.BuildStaticCall(typeof(BuiltInFunctions), Name, args[0]);

                case 2:
                    return DynamicExpressionBuilder.BuildStaticCall(typeof(BuiltInFunctions), Name, args[0], args[1]);

                case 3:
                    return DynamicExpressionBuilder.BuildStaticCall(typeof(BuiltInFunctions), Name, args[0], args[1], args[2]);

                default:
                    var message = string.Format(ErrorMessages.TooManyArguments, Name, args.Length);
                    throw new InvalidOperationException(message);
            }
        }

        public override string ToString()
        {
            var args = Args.Select(a => a.ToString());
            return Name.ToUpper() + '(' + string.Join(", ", args) + ')';
        }
    }
}
