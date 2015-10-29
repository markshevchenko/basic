﻿namespace LearningBasic.Parsing
{
    using System;

    public class ParserException : BasicException
    {
        public ParserException()
            : base()
        { }

        public ParserException(string message)
            : base(message)
        { }

        public ParserException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
