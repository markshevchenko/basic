namespace LearningBasic.Parsing.Ast.Statements
{
    using System;

    public class IfThenElse : IStatement
    {
        public IExpression Condition { get; private set; }

        public IStatement Then { get; private set; }

        public IStatement Else { get; private set; }

        public IfThenElse(IExpression condition, IStatement then)
        {
            Condition = condition;
            Then = then;
            Else = null;
        }

        public IfThenElse(IExpression condition, IStatement then, IStatement @else)
        {
            Condition = condition;
            Then = then;
            Else = @else;
        }

        public EvaluateResult Execute(IRunTimeEnvironment rte)
        {
            var conditionExpression = Condition.GetExpression(rte.Variables);
            var condition = (bool)conditionExpression.Calculate();

            if (condition)
                return Then.Execute(rte);

            if (Else != null)
                return Else.Execute(rte);

            return EvaluateResult.None;
        }

        public override string ToString()
        {
            if (Else == null)
                return "IF " + Condition + " THEN " + Then;

            return "IF " + Condition + " THEN " + Then + " ELSE " + Else;
        }
    }
}
