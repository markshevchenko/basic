namespace LearningBasic.Parsing
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// The exception tha is thrown when parsing error occured.
    /// </summary>
    [Serializable]
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
        public ParserException(string message, Exception innerException)
            : base(message, innerException)
        { }

        /// <inheritdoc />
        protected ParserException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
