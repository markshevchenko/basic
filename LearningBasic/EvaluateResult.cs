namespace LearningBasic
{
    using System;

    /// <summary>
    /// Implements a result of an Evaluate step of Real-Evaluate-Print-Loop.
    /// </summary>
    /// <remarks>
    /// The result can be described as "an optional string message".
    /// </remarks>
    public struct EvaluateResult
    {
        private readonly string message;

        /// <summary>
        /// The empty result (without a message).
        /// </summary>
        public static EvaluateResult Empty = new EvaluateResult();

        /// <summary>
        /// Indicates that the instance contains a message.
        /// </summary>
        public bool HasMessage { get { return message != null; } }

        /// <summary>
        /// Gets the message if present.
        /// </summary>
        /// <exception cref="InvalidOperationException">The instance has no value.</exception>
        public string Message
        {
            get
            {
                if (message == null)
                    throw new InvalidOperationException();

                return message;
            }
        }

        /// <summary>
        /// Initializes an instastance of the <see cref="EvaluateResult"/> with specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="message"/> is <c>null</c>.</exception>
        public EvaluateResult(string message)
            : this()
        {
            if (message == null)
                throw new ArgumentNullException("value");

            this.message = message;
        }

        /// <summary>
        /// Initializes an instastance of the <see cref="EvaluateResult"/> with specified formatted message.
        /// </summary>
        /// <param name="format">The format of the message.</param>
        /// <param name="args">The arguments.</param>
        public EvaluateResult(string format, params object[] args)
            : this(string.Format(format, args))
        { }

        /// <summary>
        /// Initializes an instastance of the <see cref="EvaluateResult"/> with
        /// string representation of the specified object.
        /// </summary>
        /// <param name="o">The object to call the <see cref="object.ToString"/> method.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="o"/> is <c>null</c>.</exception>
        public EvaluateResult(object o)
            : this()
        {
            if (o == null)
                throw new ArgumentNullException("o");

            message = o.ToString();
        }
    }
}
