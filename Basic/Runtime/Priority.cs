namespace Basic.Runtime
{
    /// <summary>
    /// Specifies operators' priority.
    /// </summary>
    public enum Priority
    {
        /// <summary>
        /// Logical OR, XOR binary operators.
        /// </summary>
        LogicalAddition,

        /// <summary>
        /// Logical AND binary operator.
        /// </summary>
        LogicalMultiplication,

        /// <summary>
        /// Logical NOT unary operator.
        /// </summary>
        LogicalNegation,

        /// <summary>
        /// Comparison operators.
        /// </summary>
        Comparison,

        /// <summary>
        /// Arithmetic ADD and SUBTRACT binary operators.
        /// </summary>
        ArithmeticAddition,

        /// <summary>
        /// Arithmetic MULTIPLY, DIVIDE, and MODULO binary operators.
        /// </summary>
        ArithmeticMultiplication,

        /// <summary>
        /// Arithmetic PLUS and MINUS unary operators.
        /// </summary>
        ArithmeticNegation,

        /// <summary>
        /// Arithmetic POWER operator.
        /// </summary>
        UpperIndex,

        /// <summary>
        /// Array-index and fucntion call operators.
        /// </summary>
        LowerIndex,

        /// <summary>
        /// Single constants, identifiers, and expressions in parentheses.
        /// </summary>
        Terminal,
    }
}
