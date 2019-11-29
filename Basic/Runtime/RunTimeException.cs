namespace Basic.Runtime
{
    using System;

    public class RunTimeException : Exception
    {
        public RunTimeException() : base() { }

        public RunTimeException(string message) : base(message) { }

        public RunTimeException(string message, Exception inner) : base(message, inner) { }
    }
}
