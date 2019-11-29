namespace Basic.Runtime.Statements
{
    using System.Collections.Generic;

    public class PrintLine : Print
    {
        public PrintLine(IEnumerable<IExpression> expressions)
            : base(expressions)
        { }

        public override EvaluateResult Execute(RunTimeEnvironment rte)
        {
            var result = base.Execute(rte);
            rte.InputOutput.WriteLine();
            return result;
        }

        public override string ToString()
        {
            return "PRINT " + string.Join(", ", Expressions);
        }
    }
}
