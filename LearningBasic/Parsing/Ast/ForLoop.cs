namespace LearningBasic.Parsing.Ast
{
    using System.Linq.Expressions;

    /// <summary>
    /// Implements <c>FOR</c> loop.
    /// </summary>
    public class ForLoop : ILoop
    {
        /// <inheritdoc />
        public bool IsOver { get; private set; }

        /// <summary>
        /// Gets the expression of the loop variable.
        /// </summary>
        public Expression Variable { get; private set; }

        /// <summary>
        /// Gets the expression of the start bound of the loop.
        /// </summary>
        public Expression From { get; private set; }

        /// <summary>
        /// Gets the expression of the end bound of the loop.
        /// </summary>
        public Expression To { get; private set; }

        /// <summary>
        /// Gets the expression of the step of the loop.
        /// </summary>
        public Expression Step { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ForLoop"/> class with the loop variable, the start bound,
        /// and the end bound.
        /// </summary>
        /// <param name="variable">The loop variable expression.</param>
        /// <param name="from">The start bound expression.</param>
        /// <param name="to">The end bound expression.</param>
        public ForLoop(Expression variable, Expression from, Expression to)
            : this(variable, from, to, Expression.Constant(1))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ForLoop"/> class with the loop variable, the start bound,
        /// the end bound, and the step.
        /// </summary>
        /// <param name="variable">The loop variable expression.</param>
        /// <param name="from">The start bound expression.</param>
        /// <param name="to">The end bound expression.</param>
        /// <param name="step">The step expression.</param>
        public ForLoop(Expression variable, Expression from, Expression to, Expression step)
        {
            Variable = variable;
            From = from;
            To = to;
            Step = step;
            IsOver = false;

            var initialize = DynamicExpressionBuilder.BuildOperator(ExpressionType.Assign, Variable, From);
            initialize.RunAndDropValue();
        }

        public void TakeStep()
        {
            var addStep = DynamicExpressionBuilder.BuildOperator(ExpressionType.AddAssign, Variable, Step);
            addStep.RunAndDropValue();

            IsOver = !IsInsideOfInterval(Variable, From, To);
        }

        /// <summary>
        /// Determines whether the variable belongs to the specified interval.
        /// </summary>
        /// <remarks>The method checks for both <c>[from, to]</c> and <c>[to, from]</c> intervals.</remarks>
        /// <param name="variable">The variable expression.</param>
        /// <param name="from">The start bound expression.</param>
        /// <param name="to">The end bound expression.</param>
        /// <returns><c>true</c> if the variable belongs to the interval; otherwise, <c>false</c>.</returns>
        public static bool IsInsideOfInterval(Expression variable, Expression from, Expression to)
        {
            var c1 = DynamicExpressionBuilder.BuildOperator(ExpressionType.GreaterThanOrEqual, variable, from);
            var c2 = DynamicExpressionBuilder.BuildOperator(ExpressionType.LessThanOrEqual, variable, to);
            var c3 = DynamicExpressionBuilder.BuildOperator(ExpressionType.GreaterThanOrEqual, variable, to);
            var c4 = DynamicExpressionBuilder.BuildOperator(ExpressionType.LessThanOrEqual, variable, from);

            var c5 = DynamicExpressionBuilder.BuildLogicalAnd(c1, c2);
            var c6 = DynamicExpressionBuilder.BuildLogicalAnd(c3, c4);

            var c7 = DynamicExpressionBuilder.BuildLogicalOr(c5, c6);

            var c8 = DynamicExpressionBuilder.BuildConvert(c7, typeof(bool));

            return (bool)c8.Calculate();
        }
    }
}
