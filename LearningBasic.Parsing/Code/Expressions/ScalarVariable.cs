namespace LearningBasic.Parsing.Code.Expressions
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
            Name = name.ToUpper();
        }

        public virtual Expression GetExpression(IDictionary<string, dynamic> variables)
        {
            var dictionary = Expression.Constant(variables);
            var name = Expression.Constant(Name);
            return Expression.MakeIndex(dictionary, DictionaryItemPropertyInfo, new[] { name });
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
