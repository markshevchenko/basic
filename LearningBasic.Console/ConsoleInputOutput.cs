namespace LearningBasic.Console
{
    using System;
    using LearningBasic.IO;

    /// <summary>
    /// Implements input and output operations for <see cref="Console">console</see>.
    /// </summary>
    public class ConsoleInputOutput : IInputOutput
    {
        /// <inheritdoc />
        public event EventHandler OnBreak;

        /// <summary>
        /// Initializes a new instance of the class <see cref="ConsoleInputOutput"/>.
        /// </summary>
        public ConsoleInputOutput()
        {
            Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs e) =>
            {
                var onBreak = OnBreak;
                if (onBreak != null)
                    onBreak(this, EventArgs.Empty);

                // Surpise, this means DO NOT CANCEL:
                e.Cancel = true;
            };
        }

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
