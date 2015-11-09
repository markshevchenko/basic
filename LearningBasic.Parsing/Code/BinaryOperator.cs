namespace LearningBasic.Parsing.Code
{
    using System.Collections.Generic;
    using System.Linq.Expressions;

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
        /// Gets the value indicating whether insert to insert a spacebar between operator sign in operands
        /// when calling the <see cref="ToString"/> method.
        /// </summary>
        /// <remarks><c>true</c> is default value.</remarks>
        protected bool DoInsertSpacebar { get; set; }

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
            DoInsertSpacebar = true;
        }

        /// <inheritdoc />
        public virtual Expression GetExpression(IDictionary<string, dynamic> variables)
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
            var left = Left.ToString();
            var right = Right.ToString();

            if (Left.Priority < Priority || (Left.Priority == Priority && Associativity == Associativity.Right))
                left = '(' + left + ')';

            if (Right.Priority < Priority || (Right.Priority == Priority && Associativity == Associativity.Left))
                right = '(' + right + ')';

            var format = DoInsertSpacebar ? "{0} {1} {2}" : "{0}{1}{2}";
            return string.Format(format, left, Operator, right);
        }
    }
}
