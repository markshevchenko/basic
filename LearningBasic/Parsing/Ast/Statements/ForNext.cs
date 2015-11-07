namespace LearningBasic.Parsing.Ast.Statements
{
    public class ForNext : For
    {
        public IStatement Statement { get; private set; }

        public ForNext(ILValue variable, IExpression from, IExpression to, IExpression step, IStatement statement)
            : base(variable, from, to, step)
        {
            Statement = statement;
        }

        public override EvaluateResult Execute(IRunTimeEnvironment rte)
        {
            EvaluateResult result;

            var loop = CreateForLoop(rte.Variables);
            do
            {
                result = Statement.Execute(rte);

                loop.TakeStep();
            }
            while (!loop.IsOver);

            return result;
        }

        public override string ToString()
        {
            return base.ToString() + " " + Statement + " NEXT";
        }
    }
}
