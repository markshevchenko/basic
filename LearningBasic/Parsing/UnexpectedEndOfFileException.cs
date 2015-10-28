namespace LearningBasic.Parsing
{
    public sealed class UnexpectedEndOfFileException : ParserException
    {
        public UnexpectedEndOfFileException()
            : base(ErrorMessages.UnexpectedEndOfFile)
        { }
    }
}
