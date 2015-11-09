namespace LearningBasic.Parsing
{
    using System;
#if !PORTABLE
    using System.Runtime.Serialization;
#endif

    /// <summary>
    /// The exception tha is thrown when parsing error occured.
    /// </summary>
#if !PORTABLE
    [Serializable]
#endif
    public class ParserException : Exception
    {
        /// <inheritdoc />
        public ParserException()
            : base()
        { }

        /// <inheritdoc />
        public ParserException(string message)
            : base(message)
        { }

        /// <inheritdoc />
        public ParserException(string message, Exception inner)
            : base(message, inner)
        { }

#if !PORTABLE
        protected ParserException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
#endif
    }
}
