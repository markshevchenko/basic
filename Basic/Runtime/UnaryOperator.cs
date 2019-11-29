namespace Basic.Runtime
{
    using System.Linq.Expressions;
    
    /// <summary>
    /// Defines a contract of unary operator.
    /// </summary>
    public abstract class UnaryOperator : IExpression
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
        /// The string representation of operator, f.e. <c>"-"</c>, or <c>"NOT"</c>.
        /// </summary>
        public string Operator { get; private set; }

        /// <summary>
        /// The single operand.
        /// </summary>
        public IExpression Operand { get; private set; }

        /// <summary>
        /// Initializes a nes instance of the <see cref="UnaryOperator"/> class with specified
        /// associativity, priority, operator, and operand.
        /// </summary>
        /// <param name="associativity">
        /// The associativity, usual <see cref="Associativity.Right"/> for prefix operators,
        /// and <see cref="Associativity.Left"/> for postfix operators.
        /// </param>
        /// <param name="priority">The priority.</param>
        /// <param name="@operator">The string representation of the operator.</param>
        /// <param name="operand">The single operand.</param>
        protected UnaryOperator(Associativity associativity, Priority priority, string @operator, IExpression operand)
        {
            Associativity = associativity;
            Priority = priority;
            Operator = @operator;
            Operand = operand;
        }

        /// <inheritdoc />
        public virtual Expression GetExpression(Variables variables)
        {
            var operand = Operand.GetExpression(variables);
            return BuildExpression(operand);
        }

        /// <summary>
        /// Builds the <see cref="Expression"/> calculating the unary operator with the specified operand.
        /// </summary>
        /// <param name="operand">The the operand.</param>
        /// <returns>The expression calculatin the operator.</returns>
        protected abstract Expression BuildExpression(Expression operand);

        /// <inheritdoc />
        public override string ToString()
        {
            return Operator + OperandAsString;
        }

        /// <summary>
        /// Gets a string representation of the <see cref="Operand"/>,
        /// enclosing it in parentheses if needed.
        /// </summary>
        protected virtual string OperandAsString
        {
            get
            {
                if (MustEncloseOperandInParentheses)
                    return "(" + Operand + ")";

                return Operand.ToString();
            }
        }

        /// <summary>
        /// Gets a value indicating whether to enclose the operand in parentheses
        /// to show the correct order of calculation.
        /// </summary>
        protected internal bool MustEncloseOperandInParentheses { get { return Priority > Operand.Priority; } }
    }
}
