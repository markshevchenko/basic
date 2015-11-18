namespace LearningInterpreter.Basic.Code.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using LearningInterpreter.RunTime;

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

        public override Expression GetExpression(Variables variables)
        {
            var arrayAsObject = base.GetExpression(variables);
            var indexes = Indexes.Select(i => i.GetExpression(variables))
                                 .Select(Subrtract1AndConvertToInt32)
                                 .ToArray();

            var array = Expression.Convert(arrayAsObject, ArrayTypes[indexes.Length]);
            return Expression.ArrayAccess(array, indexes);
        }

        private static Expression Subrtract1AndConvertToInt32(Expression e)
        {
            var subtract1 = DynamicBuilder.BuildOperator(ExpressionType.Subtract, e, Expression.Constant(1));
            var convertToInt32 = DynamicBuilder.BuildConvert(subtract1, typeof(int));
            return convertToInt32;
        }

        public override string ToString()
        {
            return string.Format("{0}[{1}]", base.ToString(), string.Join(", ", Indexes));
        }
    }
}
