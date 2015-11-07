namespace LearningBasic.Parsing.Ast.Statements
{
    using System;
    using LearningBasic.Parsing.Ast.Expressions;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Dim : IStatement
    {
        public ArrayVariable Array { get; private set; }

        public Dim(ArrayVariable array)
        {
            Array = array;
        }

        private readonly IReadOnlyDictionary<int, string> evaluateResultFormats = new Dictionary<int, string>
        {
            { 1, Messages.ArrayOfDimension1Created },
            { 2, Messages.ArrayOfDimension2Created },
            { 3, Messages.ArrayOfDimension3Created },
            { 4, Messages.ArrayOfDimension4Created },
        };

        public EvaluateResult Execute(IRunTimeEnvironment rte)
        {
            var name = Array.Name;
            var indexesAsObjects = Array.Indexes
                                        .Select(i => i.GetExpression(rte.Variables))
                                        .Select(e => e.Calculate())
                                        .ToArray();
            var indexes = indexesAsObjects.Select(o => (int)o)
                                          .ToArray();

            if (indexes.Any(index => index == 0))
                throw new InvalidOperationException(ErrorMessages.ZeroArraySize);

            if (indexes.Length < 1 || indexes.Length > 4)
                throw new InvalidOperationException(ErrorMessages.UnsupportedArrayDimension);

            rte.Variables[name] = System.Array.CreateInstance(typeof(object), indexes);

            var format = evaluateResultFormats[indexes.Length];
            return new EvaluateResult(name + format, indexesAsObjects);
        }

        public override string ToString()
        {
            return "DIM " + Array;
        }
    }
}
