namespace LearningBasic.Parsing.Ast.Statements
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Run : IStatement
    {
        public EvaluateResult Evaluate(IRunTimeEnvironment rte)
        {
            return EvaluateResult.Empty;
        }

        public override string ToString()
        {
            return "RUN";
        }
    }
}
