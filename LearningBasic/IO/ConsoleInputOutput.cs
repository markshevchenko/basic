namespace LearningBasic.IO
{
    using System;

    /// <summary>
    /// Implements input and output operations for <see cref="Console">console</see>.
    /// </summary>
    public class ConsoleInputOutput : IInputOutput
    {
        /// <inheritdoc />
        public virtual string ReadLine()
        {
            return Console.ReadLine();
        }

        /// <inheritdoc />
        public virtual void Write(string s)
        {
            Console.Write(s);
        }

        /// <inheritdoc />
        public virtual void Write(string format, params object[] args)
        {
            Console.Write(format, args);
        }

        /// <inheritdoc />
        public virtual void WriteLine(string s)
        {
            Console.WriteLine(s);
        }

        /// <inheritdoc />
        public virtual void WriteLine(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        /// <inheritdoc />
        public virtual void WriteLine()
        {
            Console.WriteLine();
        }
    }
}
