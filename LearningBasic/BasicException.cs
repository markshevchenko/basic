namespace LearningBasic
{
    using System;

    public class BasicException : Exception
    {
        public BasicException()
            : base()
        { }

        public BasicException(string message)
            : base(message)
        { }

        public BasicException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
