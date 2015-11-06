namespace LearningBasic.Parsing.Ast.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public class ArrayVariable : ScalarVariable
    {
        public IReadOnlyList<IExpression> Indexes { get; private set; }

        public override Associativity Associativity { get { return Associativity.Right; } }

        public override Priority Priority { get { return Priority.LowerIndex; } }

        public ArrayVariable(string name, IReadOnlyList<IExpression> indexes)
            : base(name)
        {
            Indexes = indexes;
        }

        private readonly IReadOnlyDictionary<int, Type> ArrayTypes = new Dictionary<int, Type>
        {
            { 1, typeof(object[]) },
            { 2, typeof(object[,]) },
            { 3, typeof(object[,,]) },
            { 4, typeof(object[,,,]) },
        };

        public override Expression GetExpression(IDictionary<string, dynamic> variables)
        {
            var arrayAsObject = base.GetExpression(variables);
            var indexes = Indexes.Select(i => i.GetExpression(variables))
                                 .Select(e => Expression.Subtract(e, Expression.Constant(1)))
                                 .ToArray();

            var array = Expression.Convert(arrayAsObject, ArrayTypes[indexes.Length]);
            return Expression.ArrayAccess(array, indexes);
        }

        public override string ToString()
        {
            return string.Format("{0}[{1}]", base.ToString(), string.Join(", ", Indexes));
        }
    }
}
