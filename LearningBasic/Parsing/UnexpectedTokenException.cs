namespace Basic.Parsing
{
    using System;

    public class UnexpectedTokenException : ParserException
    {
        public UnexpectedTokenException(object expectedToken, object actualToken)
            : base(ErrorMessages.UnexpectedToken, expectedToken, actualToken)
        { }
    }
}
