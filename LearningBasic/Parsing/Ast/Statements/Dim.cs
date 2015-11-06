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

        public EvaluateResult Execute(IRunTimeEnvironment rte)
        {
            var name = Array.Name;
            var indexes = Array.Indexes
                               .Select(i => i.GetExpression(rte.Variables))
                               .Select(e => (int)e.CalculateValue())
                               .ToArray();

            switch (indexes.Length)
            {
                case 1:
                    rte.Variables[name] = System.Array.CreateInstance(typeof(object), indexes[0]);
                    return new EvaluateResult(Messages.ArrayOfDimension1Created, indexes[0]);

                case 2:
                    rte.Variables[name] = System.Array.CreateInstance(typeof(object), indexes[0], indexes[1]);
                    return new EvaluateResult(Messages.ArrayOfDimension2Created, indexes[0], indexes[1]);

                case 3:
                    rte.Variables[name] = System.Array.CreateInstance(typeof(object), indexes[0], indexes[1], indexes[2]);
                    return new EvaluateResult(Messages.ArrayOfDimension3Created, indexes[0], indexes[1], indexes[2]);

                case 4:
                    rte.Variables[name] = System.Array.CreateInstance(typeof(object), indexes[0], indexes[1], indexes[2], indexes[3]);
                    return new EvaluateResult(Messages.ArrayOfDimension4Created, indexes[0], indexes[1], indexes[2], indexes[3]);

                default:
                    throw new ParserException(ErrorMessages.UnsupportedArrayDimension);
            }
        }

        public override string ToString()
        {
            return "DIM " + Array;
        }
    }
}
