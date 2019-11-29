namespace Basic.Runtime
{
    using System;
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
            if (variable == null)
                throw new ArgumentNullException(nameof(variable));

            if (from == null)
                throw new ArgumentNullException(nameof(from));

            if (to == null)
                throw new ArgumentNullException(nameof(to));

            if (step == null)
                throw new ArgumentNullException(nameof(step));

            Variable = variable;
            From = from;
            To = to;
            Step = step;
            IsOver = false;

            var source = From.Type == typeof(object) ? From : Expression.Convert(From, typeof(object));
            var assign = Expression.Assign(Variable, source);
            assign.RunAndDropValue();
        }

        public void TakeStep()
        {
            var addStep = DynamicBuilder.BuildOperator(ExpressionType.Add, Variable, Step);
            var convert = DynamicBuilder.BuildConvert(addStep, typeof(object));
            var assign = Expression.Assign(Variable, convert);

            assign.RunAndDropValue();

            IsOver = !IsBelongsToInterval(Variable, From, To);
        }

        /// <summary>
        /// Determines whether the variable belongs to the specified interval.
        /// </summary>
        /// <remarks>The method checks for both <c>[from, to]</c> and <c>[to, from]</c> intervals.</remarks>
        /// <param name="variable">The variable expression.</param>
        /// <param name="from">The start bound expression.</param>
        /// <param name="to">The end bound expression.</param>
        /// <returns><c>true</c> if the variable belongs to the interval; otherwise, <c>false</c>.</returns>
        internal static bool IsBelongsToInterval(Expression variable, Expression from, Expression to)
        {
            var isBelongsToDirectInterval = BelongsToDirectInterval(variable, from, to);
            var isBelongsToInverseInterval = BelongsToInverseInterval(variable, from, to);

            var result = DynamicBuilder.BuildLogicalOr(isBelongsToDirectInterval, isBelongsToInverseInterval);

            var resultAsBool = (bool)DynamicBuilder.BuildConvert(result, typeof(bool))
                                                      .Calculate();

            return resultAsBool;
        }

        private static Expression BelongsToDirectInterval(Expression variable, Expression from, Expression to)
        {
            var greaterThanOrEqualFrom = DynamicBuilder.BuildOperator(ExpressionType.GreaterThanOrEqual, variable, from);
            var lessThanOrEqualTo = DynamicBuilder.BuildOperator(ExpressionType.LessThanOrEqual, variable, to);

            return DynamicBuilder.BuildLogicalAnd(greaterThanOrEqualFrom, lessThanOrEqualTo);
        }

        private static Expression BelongsToInverseInterval(Expression variable, Expression from, Expression to)
        {
            var greaterThanOrEqualTo = DynamicBuilder.BuildOperator(ExpressionType.GreaterThanOrEqual, variable, to);
            var lessThanOrEqualFrom = DynamicBuilder.BuildOperator(ExpressionType.LessThanOrEqual, variable, from);

            return DynamicBuilder.BuildLogicalAnd(greaterThanOrEqualTo, lessThanOrEqualFrom);
        }
    }
}
