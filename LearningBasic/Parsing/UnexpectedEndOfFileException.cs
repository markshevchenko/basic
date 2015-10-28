namespace Basic.Parsing
{
    using System;

    public class UnexpectedEndOfFileException : ParserException
    {
        public UnexpectedEndOfFileException()
            : base(ErrorMessages.UnexpectedEndOfFile)
        { }
    }
}
