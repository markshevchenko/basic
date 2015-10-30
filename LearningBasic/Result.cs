namespace LearningBasic
{
    using System;

    public struct Result
    {
        private readonly string value;

        public static Result Nothing = new Result();

        public bool HasValue { get { return value != null; } }

        public string Value
        {
            get
            {
                if (value == null)
                    throw new InvalidOperationException();

                return value;
            }
        }

        public Result(string value)
            : this()
        {
            if (value == null)
                throw new ArgumentNullException("value");

            this.value = value;
        }

        public Result(object o)
            : this()
        {
            if (o == null)
                throw new ArgumentNullException("o");

            value = o.ToString();
        }
    }
}
