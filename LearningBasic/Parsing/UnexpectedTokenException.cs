namespace LearningBasic.Parsing
{
    using System;

    /// <summary>
    /// The exception tha is thrown when unexpected token read.
    /// </summary>
    [Serializable]
    public sealed class UnexpectedTokenException : ParserException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnexpectedTokenException"/>
        /// with an expected token, and an actual token.
        /// </summary>
        /// <param name="expectedToken">The expected token.</param>
        /// <param name="actualToken">The actual token.</param>
        public UnexpectedTokenException(object expectedToken, object actualToken)
            : base(string.Format(ErrorMessages.UnexpectedToken, expectedToken, actualToken))
        { }
    }
}
