namespace LearningBasic.Parsing.Ast.Statements
{
    public class Goto : IStatement
    {
        public IExpression Number { get; private set; }

        public Goto(IExpression number)
        {
            Number = number;
        }

        public EvaluateResult Execute(IRunTimeEnvironment rte)
        {
            var numberExpression = Number.GetExpression(rte.Variables);
            var number = numberExpression.Calculate();

            rte.Goto((int)number);

            return EvaluateResult.None;
        }

        public override string ToString()
        {
            return "GOTO " + Number;
        }
    }
}
