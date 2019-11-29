namespace Basic.IO
{
    using System;

    public class IOException : Exception
    {
        public IOException() : base() { }

        public IOException(string message) : base(message) { }

        public IOException(string message, Exception inner) : base(message, inner) { }
    }
}
