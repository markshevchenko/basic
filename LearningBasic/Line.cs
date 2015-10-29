namespace LearningBasic
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents a program line that is a statement with a possible line number.
    /// </summary>
    /// <typeparam name="TTag">The type of a tag of an Abstract Syntax Tree.</typeparam>
    public class Line<TTag>
        where TTag : struct
    {
        private const int StatementIndex = 0;
        public const int MinNumber = 1;
        public const int MaxNumber = 99999;

        /// <summary>
        /// The line number.
        /// </summary>
        /// <remarks><c>null</c> value means a line without a number.</remarks>
        public int? Number { get; private set; }

        /// <summary>
        /// The statement.
        /// </summary>
        public AstNode<TTag> Statement { get; private set; }

        /// <summary>
        /// Creates an instance of <see cref="Line"/>.
        /// </summary>
        /// <param name="line">The root node of a line's Abstract Syntax Tree.</param>
        public Line(AstNode<TTag> line)
        {
            if (line == null)
                throw new ArgumentNullException("line");

            Number = ParseNumber(line.Text);
            Statement = line.Children[StatementIndex];
        }

        /// <summary>
        /// Parses the number of a BASIC's line.
        /// </summary>
        /// <param name="numberAsString">The line number as a <see cref="String">string</see>.</param>
        /// <returns>The line number, or <c>null</c> if <paramref name="numberAsString"/> is null or empty.</returns>
        /// <exception cref="EvaluatorException">
        /// The number can't be parsed, or is out of range of <see cref="MinNumber"/> to <see cref="MaxNumber"/>.
        /// </exception>
        public static int? ParseNumber(string numberAsString)
        {
            if (string.IsNullOrEmpty(numberAsString))
                return null;

            var number = ParseInt32AndWrapException(numberAsString);
            if (number < MinNumber || number > MaxNumber)
            {
                var message = string.Format(ErrorMessages.LineNumberOutOfRange, MinNumber, MaxNumber);
                throw new BasicException(message);
            }

            return number;
        }

        /// <summary>
        /// Converts the string representation of a number to its 32-bit signed integer equivalent
        /// </summary>
        /// <param name="s">A string containing a number to convert.</param>
        /// <returns>A 32-bit signed integer equivalent to the number contained in <paramref name="s"/>.</returns>
        /// <exception cref="EvaluatorException">Any exception occured while converting <paramref name="s"/>.</exception>
        public static int ParseInt32AndWrapException(string s)
        {
            try
            {
                return int.Parse(s, NumberStyles.Integer, CultureInfo.InvariantCulture);
            }
            catch (Exception exception)
            {
                var message = string.Format(ErrorMessages.CantParseLineNumber, s);
                throw new BasicException(message, exception);
            }
        }
    }
}
