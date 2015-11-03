namespace LearningBasic.Parsing.Ast.Expressions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

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

        public override Expression GetExpression(IDictionary<string, dynamic> variables)
        {
            var array = base.GetExpression(variables);
            var indexes = Indexes.Select(i => i.GetExpression(variables))
                                 .Select(e => Expression.Add(e, Expression.Constant(1)))
                                 .ToArray();
            return Expression.ArrayAccess(array, indexes);
        }

        public override string ToString()
        {
            return string.Format("{0}[{1}]", base.ToString(), string.Join(", ", Indexes));
        }
    }
}
