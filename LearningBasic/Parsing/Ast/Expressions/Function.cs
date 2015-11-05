namespace LearningBasic.Parsing.Ast.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class Function : NaryExpression
    {
        public string Name { get; private set; }

        public IReadOnlyList<IExpression> Args { get; private set; }

        public Function(string name, params IExpression[] args)
            : base(Associativity.Left, Priority.LowerIndex)
        {
            Name = name.ToUpper();
            Args = args;
        }

        public Function(string name, IReadOnlyList<IExpression> args)
            : base(Associativity.Left, Priority.LowerIndex)
        {
            Name = name.ToUpper();
            Args = args;
        }

        public override Expression GetExpression(IDictionary<string, dynamic> variables)
        {
            var args = Args.Select(p => p.GetExpression(variables))
                           .ToArray();

            switch (args.Length)
            {
                case 0:
                    return CallStaticMethod(typeof(BuiltInFunctions), Name);

                case 1:
                    return CallStaticMethod(typeof(BuiltInFunctions), Name, args[0]);

                case 2:
                    return CallStaticMethod(typeof(BuiltInFunctions), Name, args[0], args[1]);

                case 3:
                    return CallStaticMethod(typeof(BuiltInFunctions), Name, args[0], args[1], args[2]);

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
