namespace LearningBasic.Parsing.Ast.Statements
{
    using System;

    public class ForNext : For
    {
        public IStatement Statement { get; private set; }

        public ForNext(ILValue loopVariable, IExpression from, IExpression to, IExpression step, IStatement statement)
            : base(loopVariable, from, to, step)
        {
            Statement = null;
        }

        public override EvaluateResult Execute(IRunTimeEnvironment rte)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return base.ToString() + " " + Statement + " NEXT";
        }
    }
}
