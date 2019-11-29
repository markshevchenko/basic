namespace Basic.Runtime.Statements
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Basic.Runtime.Expressions;

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

        public EvaluateResult Execute(RunTimeEnvironment rte)
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

            rte.Variables[name] = CreateArrayAndFillWithZeros(indexes);

            var format = evaluateResultFormats[indexes.Length];
            return new EvaluateResult(name + format, indexesAsObjects);
        }

        private System.Array CreateArrayAndFillWithZeros(int[] indexes)
        {
            var array = System.Array
                              .CreateInstance(typeof(object), indexes);

            array.Fill(0);

            return array;
        }

        public override string ToString()
        {
            return "DIM " + Array;
        }
    }
}
