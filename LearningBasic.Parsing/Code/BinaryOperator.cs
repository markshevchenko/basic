namespace LearningBasic.Parsing.Code
{
    using System.Linq.Expressions;
    using LearningBasic.RunTime;

    /// <summary>
    /// Defines a contract of binary operator.
    /// </summary>
    public abstract class BinaryOperator : IExpression
    {
        /// <summary>
        /// Gets the associativity of expression.
        /// </summary>
        public Associativity Associativity { get; private set; }

        /// <summary>
        /// Gets the priority of expression
        /// </summary>
        public Priority Priority { get; private set; }

        /// <summary>
        /// The string representation of operator, f.e. <c>"+"</c>, or <c>"AND"</c>.
        /// </summary>
        public string Operator { get; private set; }

        /// <summary>
        /// The left operand.
        /// </summary>
        public IExpression Left { get; private set; }

        /// <summary>
        /// The right operand.
        /// </summary>
        public IExpression Right { get; private set; }

        /// <summary>
        /// Initializes a nes instance of the <see cref="BinaryOperator"/> class with specified
        /// associativity, priority, operator, left, and right operands.
        /// </summary>
        /// <param name="associativity">
        /// The associativity (usual <see cref="Associativity.Right"/> for power operator,
        /// and <see cref="Associativity.Left"/> for all the others.
        /// </param>
        /// <param name="priority">The priority.</param>
        /// <param name="@operator">The string representation of the operator.</param>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        protected BinaryOperator(Associativity associativity, Priority priority, string @operator, IExpression left, IExpression right)
        {
            Associativity = associativity;
            Priority = priority;
            Operator = @operator;
            Left = left;
            Right = right;
        }

        /// <inheritdoc />
        public virtual Expression GetExpression(Variables variables)
        {
            var left = Left.GetExpression(variables);
            var right = Right.GetExpression(variables);
            return BuildExpression(left, right);
        }

        /// <summary>
        /// Builds the <see cref="Expression"/> calculating the binary operator with the specified operands.
        /// </summary>
        /// <param name="left">The the left operand.</param>
        /// <param name="right">The the right operand.</param>
        /// <returns>The expression calculatin the operator.</returns>
        protected abstract Expression BuildExpression(Expression left, Expression right);

        /// <inheritdoc />
        public override string ToString()
        {
            return LeftAsString + " " + Operator + " " + RightAsString;
        }

        /// <summary>
        /// Gets a string representation of the <see cref="Left"/> operand,
        /// enclosing it in parentheses if needed.
        /// </summary>
        protected virtual string LeftAsString
        {
            get
            {
                if (MustEncloseLeftOperandInParentheses)
                    return "(" + Left + ")";

                return Left.ToString();
            }
        }

        /// <summary>
        /// Gets a string representation of the <see cref="Right"/> operand,
        /// enclosing it in parentheses if needed.
        /// </summary>
        protected virtual string RightAsString
        {
            get
            {
                if (MustEncloseRightOperandInParentheses)
                    return "(" + Right + ")";

                return Right.ToString();
            }
        }

        /// <summary>
        /// Gets a value indicating whether to enclose the left operand in parentheses
        /// to show the correct order of calculation.
        /// </summary>
        protected internal bool MustEncloseLeftOperandInParentheses
        {
            get { return Left.Priority < Priority || (Left.Priority == Priority && Associativity == Associativity.Right); }
        }

        /// <summary>
        /// Gets a value indicating whether to enclose the right operand in parentheses
        /// to show the correct order of calculation.
        /// </summary>
        protected internal bool MustEncloseRightOperandInParentheses
        {
            get { return Right.Priority < Priority || (Right.Priority == Priority && Associativity == Associativity.Left); }
        }
    }
}
