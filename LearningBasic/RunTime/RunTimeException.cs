namespace LearningBasic.RunTime
{
    using System;
#if !PORTABLE
    using System.Runtime.Serialization;
#endif

    /// <summary>
    /// The exception that is thrown at the run-time.
    /// </summary>
#if !PORTABLE
    [Serializable]
#endif
    public class RunTimeException : Exception
    {
        public RunTimeException()
            : base()
        { }

        public RunTimeException(string message)
            : base(message)
        { }

        public RunTimeException(string message, Exception inner)
            : base(message, inner)
        { }

#if !PORTABLE
        protected ParserException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
#endif
    }
}
