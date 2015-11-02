namespace LearningBasic.Parsing.Ast.Statements
{
    using System;

    public class Next : IStatement
    {
        public EvaluateResult Run(IRunTimeEnvironment rte)
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return "NEXT";
        }
    }
}
