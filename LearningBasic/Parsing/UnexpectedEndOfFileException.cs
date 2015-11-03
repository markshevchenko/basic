namespace LearningBasic.Parsing
{
    using System;

    /// <summary>
    /// The exception tha is thrown when unexpected end of file read.
    /// </summary>
    [Serializable]
    public sealed class UnexpectedEndOfFileException : ParserException
    {
        /// <inheritdoc />
        public UnexpectedEndOfFileException()
            : base(ErrorMessages.UnexpectedEndOfFile)
        { }
    }
}
