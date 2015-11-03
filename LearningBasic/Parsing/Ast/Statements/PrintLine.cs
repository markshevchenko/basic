namespace LearningBasic.Parsing.Ast.Statements
{
    using System.Collections.Generic;

    public class PrintLine : Print
    {
        public PrintLine(IEnumerable<IExpression> expressions)
            : base(expressions)
        { }

        public override EvaluateResult Evaluate(IRunTimeEnvironment rte)
        {
            var result = base.Evaluate(rte);
            rte.InputOutput.WriteLine();
            return result;
        }

        public override string ToString()
        {
            return "PRINT " + string.Join(", ", Expressions);
        }
    }
}
