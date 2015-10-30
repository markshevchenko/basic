namespace LearningBasic
{
    using System;

    public struct StatementResult
    {
        private readonly string message;

        public static StatementResult Empty = new StatementResult();

        public bool HasMessage { get { return message != null; } }

        public string Message
        {
            get
            {
                if (message == null)
                    throw new InvalidOperationException();

                return message;
            }
        }

        public StatementResult(string value)
            : this()
        {
            if (value == null)
                throw new ArgumentNullException("value");

            this.message = value;
        }

        public StatementResult(object o)
            : this()
        {
            if (o == null)
                throw new ArgumentNullException("o");

            message = o.ToString();
        }
    }
}
