namespace LearningBasic.Parsing.Ast.Statements
{
    using System;

    public class Next : IStatement
    {
        public EvaluateResult Execute(IRunTimeEnvironment rte)
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return "NEXT";
        }
    }
}
