namespace LearningBasic.IO
{
    using System;
#if !PORTABLE
    using System.Runtime.Serialization;
#endif

    /// <summary>
    /// The exception that is thrown when IO operation occured.
    /// </summary>
#if !PORTABLE
    [Serializable]
#endif
    public class IOException : Exception
    {
        /// <inheritdoc />
        public IOException()
            : base()
        { }

        /// <inheritdoc />
        public IOException(string message)
            : base(message)
        { }

        /// <inheritdoc />
        public IOException(string message, Exception inner)
            : base(message, inner)
        { }

#if !PORTABLE
        protected IOException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
#endif
    }
}
