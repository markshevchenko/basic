namespace LearningBasic.Compiling
{
    using System;

    public class CompilerException : Exception
    {
        public CompilerException()
            : base()
        { }

        public CompilerException(string message)
            : base(message)
        { }

        public CompilerException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
