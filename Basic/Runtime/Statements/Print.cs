namespace Basic.Runtime.Statements
{
    using System;
    using System.Collections.Generic;

    public class Print : IStatement
    {
        public IReadOnlyList<IExpression> Expressions { get; private set; }

        public Print(IEnumerable<IExpression> expressions)
        {
            if (expressions == null)
                throw new ArgumentNullException(nameof(expressions));

            Expressions = new List<IExpression>(expressions);
        }

        public virtual EvaluateResult Execute(RunTimeEnvironment rte)
        {
            foreach (var expression in Expressions)
            {
                var compiled = expression.GetExpression(rte.Variables);
                var value = compiled.Calculate();
                var valueAsString = value == null ? string.Empty : value.ToString();
                rte.InputOutput.Write(valueAsString);
            }

            return EvaluateResult.None;
        }

        public override string ToString()
        {
            return "PRINT " + string.Join(", ", Expressions) + ';';
        }
    }
}
