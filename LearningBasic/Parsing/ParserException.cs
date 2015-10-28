namespace Basic.Parsing
{
    using System;

    public class ParserException : Exception
    {
        public ParserException(string format, params object[] args)
            : base(string.Format(format, args))
        { }
    }
}
