namespace LearningBasic.RunTime
{
    using System;

    /// <summary>
    /// Implements a result of an Evaluate step of Real-Evaluate-Print-Loop.
    /// </summary>
    /// <remarks>
    /// The result can be described as "an optional string message" i.e. as it were the <see cref="Nullable{T}"/> of <see cref="String"/>.
    /// </remarks>
    public struct EvaluateResult : IEquatable<EvaluateResult>
    {
        private readonly string message;

        /// <summary>
        /// The none result (without message).
        /// </summary>
        public static EvaluateResult None = new EvaluateResult();

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

        /// <inheritdoc />
        public bool Equals(EvaluateResult other)
        {
            return message == other.message;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is EvaluateResult)
                return Equals((EvaluateResult)obj);

            return false;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return message;
        }

        /// <inheritdoc />
        public static bool operator ==(EvaluateResult a, EvaluateResult b)
        {
            return a.message == b.message;
        }

        /// <inheritdoc />
        public static bool operator !=(EvaluateResult a, EvaluateResult b)
        {
            return a.message != b.message;
        }
    }
}
