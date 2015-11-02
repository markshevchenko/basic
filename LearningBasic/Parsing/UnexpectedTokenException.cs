namespace LearningBasic.Parsing
{
    using System;

    [Serializable]
    public sealed class UnexpectedTokenException : ParserException
    {
        public UnexpectedTokenException(object expectedToken, object actualToken)
            : base(string.Format(ErrorMessages.UnexpectedToken, expectedToken, actualToken))
        { }
    }
}
