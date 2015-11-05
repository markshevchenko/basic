namespace LearningBasic.Evaluating
{
    using System;

    /// <summary>
    /// The exception that is thrown when run-time occured.
    /// </summary>
    [Serializable]
    public class RunTimeException : Exception
    {
        public RunTimeException()
            : base()
        { }

        public RunTimeException(string message)
            : base(message)
        { }

        public RunTimeException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
