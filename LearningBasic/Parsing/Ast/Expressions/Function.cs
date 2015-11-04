using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LearningBasic.Parsing.Ast.Expressions
{
    public class Function : NaryExpression
    {
        public string Name { get; private set; }

        public IExpression[] Parameters { get; private set; }

        public Function(string name, params IExpression[] parameters)
            : base(Associativity.Left, Priority.LowerIndex)
        {
            Name = name;
            Parameters = parameters;
        }

        public override Expression GetExpression(IDictionary<string, dynamic> variables)
        {
            var args = Parameters.Select(p => p.GetExpression(variables))
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
    }
}
