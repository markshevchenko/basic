namespace LearningBasic.Parsing.Ast.Statements
{
    public class Goto : IStatement
    {
        public IExpression Number { get; private set; }

        public Goto(IExpression number)
        {
            Number = number;
        }

        public EvaluateResult Evaluate(IRunTimeEnvironment rte)
        {
            var numberExpression = Number.GetExpression(rte.Variables);
            var number = numberExpression.CalculateValue();

            rte.Goto((int)number);

            return EvaluateResult.Empty;
        }

        public override string ToString()
        {
            return "GOTO " + Number.ToPrintable();
        }
    }
}
