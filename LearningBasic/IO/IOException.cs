namespace LearningBasic.IO
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// The exception that is thrown when IO operation occured.
    /// </summary>
    [Serializable]
    public class IOException : Exception
    {
        /// <summary>
        /// Gets IO operation that leads to error.
        /// </summary>
        public Operation Operation { get; private set; }

        /// <summary>
        /// Gets file name when <see cref="Operation"/> is <see cref="Operation.Save"/>
        /// or <see cref="Operation.Load"/>.
        /// </summary>
        /// <remarks>
        /// <c>null</c>, if <see cref="Operation"/> is not <see cref="Operation.Save"/>
        /// or <see cref="Operation.Load"/>
        /// </remarks>
        public string FileName { get; private set; }

        /// <inheritdoc />
        public IOException()
            : base()
        {
            Operation = Operation.Unknown;
            FileName = null;
        }

        /// <inheritdoc />
        public IOException(string message)
            : base(message)
        {
            Operation = Operation.Unknown;
            FileName = null;
        }

        /// <inheritdoc />
        public IOException(string message, Exception innerException)
            : base(message, innerException)
        {
            Operation = Operation.Unknown;
            FileName = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IOException"/> class with an operation
        /// that leads to error, a file name, an error message, and a reference to the inner
        /// exception that is the cause of this exception.
        /// </summary>
        /// <param name="operation">The IO operation that leads to error.</param>
        /// <param name="fileName">The file name.</param>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public IOException(Operation operation, string fileName, string message, Exception innerException)
            : base(message, innerException)
        {
            Operation = operation;
            FileName = fileName;
        }

        /// <inheritdoc />
        protected IOException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Operation = (Operation)info.GetValue("Operation", typeof(Operation));
            FileName = info.GetString("FileName");
        }

        /// <inheritdoc />
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Operation", Operation, typeof(Operation));
            info.AddValue("FileName", FileName);
        }
    }
}
