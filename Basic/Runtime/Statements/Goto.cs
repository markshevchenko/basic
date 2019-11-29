namespace Basic.Runtime.Statements
{
    public class Goto : IStatement
    {
        public IExpression Number { get; private set; }

        public Goto(IExpression number)
        {
            Number = number;
        }

        public EvaluateResult Execute(RunTimeEnvironment rte)
        {
            var numberExpression = Number.GetExpression(rte.Variables);
            var number = numberExpression.Calculate();
            var label = number.ToString();

            rte.Goto(label);

            return EvaluateResult.None;
        }

        public override string ToString()
        {
            return "GOTO " + Number;
        }
    }
}
