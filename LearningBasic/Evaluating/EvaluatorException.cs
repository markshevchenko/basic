namespace LearningBasic.Evaluating
{
    using System;

    public class EvaluatorException : Exception
    {
        public EvaluatorException()
            : base()
        { }

        public EvaluatorException(string message)
            : base(message)
        { }

        public EvaluatorException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
