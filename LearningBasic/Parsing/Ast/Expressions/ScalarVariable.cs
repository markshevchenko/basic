namespace LearningBasic.Parsing.Ast.Expressions
{
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    public class ScalarVariable : ILValue
    {
        private static readonly PropertyInfo DictionaryItemPropertyInfo = typeof(IDictionary<string, object>).GetProperty("Item");

        public string Name { get; private set; }

        public virtual Associativity Associativity { get { return Associativity.Left; } }

        public virtual Priority Priority { get { return Priority.Terminal; } }

        public ScalarVariable(string name)
        {
            Name = name;
        }

        public virtual Expression Compile(IRunTimeEnvironment rte)
        {
            var variables = Expression.Constant(rte.Variables);
            var name = Expression.Constant(Name);
            return Expression.MakeIndex(variables, DictionaryItemPropertyInfo, new[] { name });
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
