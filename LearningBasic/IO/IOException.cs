namespace LearningBasic.IO
{
    using System;

    [Serializable]
    public class IOException : Exception
    {
        public IOException()
            : base()
        { }

        public IOException(string message)
            : base(message)
        { }

        public IOException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
