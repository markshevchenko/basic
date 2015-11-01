namespace LearningBasic.Parsing
{
    using System;

    [Serializable]
    public sealed class UnexpectedEndOfFileException : ParserException
    {
        public UnexpectedEndOfFileException()
            : base(ErrorMessages.UnexpectedEndOfFile)
        { }
    }
}
