namespace LearningBasic.Parsing.Ast.Statements
{
    using System;

    public class Next : IStatement
    {
        public Result Run(IRunTimeEnvironment rte)
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return "NEXT";
        }
    }
}
