namespace LearningBasic.Parsing.Ast.Statements
{
    using System;

    public class Next : IStatement
    {
        public EvaluateResult Evaluate(IRunTimeEnvironment rte)
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return "NEXT";
        }
    }
}
