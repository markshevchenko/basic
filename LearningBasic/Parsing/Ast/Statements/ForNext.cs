namespace LearningBasic.Parsing.Ast.Statements
{
    using System.Linq.Expressions;

    public class ForNext : For
    {
        public IStatement Statement { get; private set; }

        public ForNext(ILValue loopVariable, IExpression from, IExpression to, IExpression step, IStatement statement)
            : base(loopVariable, from, to, step)
        {
            Statement = statement;
        }

        public override EvaluateResult Execute(IRunTimeEnvironment rte)
        {
            var loopVariable = LoopVariable.GetExpression(rte.Variables);
            var from = From.GetExpression(rte.Variables);
            var to = To.GetExpression(rte.Variables);
            var step = Step == null ? Expression.Constant(1) : Step.GetExpression(rte.Variables);

            // Initialization
            Expression.Assign(loopVariable, Expression.Convert(from, typeof(object))).CalculateValue();

            while (true)
            {
                // Check condition

                var c1 = DynamicExpressionBuilder.BuildOperator(ExpressionType.GreaterThanOrEqual, loopVariable, from);
                var c2 = DynamicExpressionBuilder.BuildOperator(ExpressionType.LessThanOrEqual, loopVariable, to);
                var c3 = DynamicExpressionBuilder.BuildOperator(ExpressionType.GreaterThanOrEqual, loopVariable, to);
                var c4 = DynamicExpressionBuilder.BuildOperator(ExpressionType.LessThanOrEqual, loopVariable, from);

                var c5 = DynamicExpressionBuilder.BuildLogicalAnd(c1, c2);
                var c6 = DynamicExpressionBuilder.BuildLogicalAnd(c3, c4);

                var c7 = DynamicExpressionBuilder.BuildLogicalOr(c5, c6);

                var c8 = DynamicExpressionBuilder.BuildConvert(c7, typeof(bool));

                var doContinue = (bool)c8.CalculateValue();

                if (!doContinue)
                    break;

                // Body

                Statement.Execute(rte);

                // Increment
                var e1 = DynamicExpressionBuilder.BuildOperator(ExpressionType.Add, loopVariable, step);
                var e2 = Expression.Convert(e1, typeof(object));
                var e3 = Expression.Assign(loopVariable, e2);

                e3.CalculateValue();
            }

            return EvaluateResult.None;
        }

        public override string ToString()
        {
            return base.ToString() + " " + Statement + " NEXT";
        }
    }
}
