namespace LearningBasic
{
    using System;

    /// <summary>
    /// Describes input and output operations required by the BASIC interpreter.
    /// </summary>
    public interface IInputOutput
    {
        /// <summary>
        /// Occurs when the Ctrl+C or Ctrl+Break key combination are pressed.
        /// </summary>
        event EventHandler OnBreak;

        /// <summary>
        /// Reads a single line from the input.
        /// </summary>
        /// <returns>The line read.</returns>
        string ReadLine();

        /// <summary>
        /// Writes a string to the output.
        /// </summary>
        /// <param name="s">The string to write.</param>
        void Write(string s);

        /// <summary>
        /// Formats string and writes it to the output.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        void Write(string format, params object[] args);

        /// <summary>
        /// Writes a string to the output and starts new line.
        /// </summary>
        /// <param name="s">The string to write.</param>
        void WriteLine(string s);

        /// <summary>
        /// Formats string, writes it to the output, and starts new line.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        void WriteLine(string format, params object[] args);

        /// <summary>
        /// Starts new line.
        /// </summary>
        void WriteLine();
    }
}
