namespace LearningBasic.Parsing.Code.Statements
{
    using System.Collections.Generic;
    using LearningBasic.RunTime;

    public class For : IStatement
    {
        public ILValue Variable { get; private set; }

        public IExpression From { get; private set; }

        public IExpression To { get; private set; }

        public IExpression Step { get; private set; }

        public For(ILValue variable, IExpression from, IExpression to, IExpression step)
        {
            Variable = variable;
            From = from;
            To = to;
            Step = step;
        }

        public virtual EvaluateResult Execute(IRunTimeEnvironment rte)
        {
            var loop = CreateForLoop(rte.Variables);

            rte.StartLoop(loop);

            return EvaluateResult.None;
        }

        public override string ToString()
        {
            if (Step == null)
                return "FOR " + Variable + " = " + From + " TO " + To;

            return "FOR " + Variable + " = " + From + " TO " + To + " STEP " + Step;
        }

        protected virtual ForLoop CreateForLoop(IDictionary<string, dynamic> variables)
        {
            var variable = Variable.GetExpression(variables);
            var from = From.GetExpression(variables);
            var to = To.GetExpression(variables);

            if (Step == null)
                return new ForLoop(variable, from, to);

            var step = Step.GetExpression(variables);

            return new ForLoop(variable, from, to, step);
        }
    }
}
