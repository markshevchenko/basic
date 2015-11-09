namespace LearningBasic.Parsing.Code.Statements
{
    using System.Collections.Generic;
    using LearningBasic.RunTime;

    public class PrintLine : Print
    {
        public PrintLine(IEnumerable<IExpression> expressions)
            : base(expressions)
        { }

        public override EvaluateResult Execute(IRunTimeEnvironment rte)
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
