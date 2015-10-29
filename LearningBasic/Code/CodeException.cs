namespace LearningBasic.Code
{
    using System;

    public class CodeException : BasicException
    {
        public CodeException()
            : base()
        { }

        public CodeException(string message)
            : base(message)
        { }

        public CodeException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
