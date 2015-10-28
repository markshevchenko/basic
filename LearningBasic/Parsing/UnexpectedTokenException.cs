namespace LearningBasic.Parsing
{
    public sealed class UnexpectedTokenException : ParserException
    {
        public UnexpectedTokenException(object expectedToken, object actualToken)
            : base(string.Format(ErrorMessages.UnexpectedToken, expectedToken, actualToken))
        { }
    }
}
