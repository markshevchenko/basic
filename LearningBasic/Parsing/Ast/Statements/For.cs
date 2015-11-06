namespace LearningBasic.Parsing.Ast.Statements
{
    using System;

    public class For : IStatement
    {
        public ILValue LoopVariable { get; private set; }

        public IExpression From { get; private set; }

        public IExpression To { get; private set; }

        public IExpression Step { get; private set; }

        public For(ILValue loopVariable, IExpression from, IExpression to, IExpression step)
        {
            LoopVariable = loopVariable;
            From = from;
            To = to;
            Step = step;
        }

        public virtual EvaluateResult Execute(IRunTimeEnvironment rte)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            if (Step == null)
                return "FOR " + LoopVariable + " = " + From + " TO " + To;

            return "FOR " + LoopVariable + " = " + From + " TO " + To + " STEP " + Step;
        }
    }
}
