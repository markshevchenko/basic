namespace LearningBasic.Parsing.Ast
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents a program line that is a statement with a possible line number.
    /// </summary>
    public class Line : ILine
    {
        public const int MinNumber = 1;
        public const int MaxNumber = 99999;

        public int? Number { get; private set; }

        public IStatement Statement { get; private set; }

        public Line(IStatement statement)
        {
            if (statement == null)
                throw new ArgumentNullException("statement");

            Number = null;
            Statement = statement;
        }

        /// <summary>
        /// Creates an instance of <see cref="Line"/> with line number.
        /// </summary>
        /// <param name="number">The string representation of line number.</param>
        /// <param name="statement">The statement.</param>
        public Line(string number, IStatement statement)
        {
            if (number == null)
                throw new ArgumentNullException("number");

            if (statement == null)
                throw new ArgumentNullException("statement");

            Number = ParseNumber(number);
            Statement = statement;
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
            var number = ParseInt32AndWrapException(numberAsString);
            if (number < MinNumber || number > MaxNumber)
            {
                var message = string.Format(ErrorMessages.LineNumberOutOfRange, MinNumber, MaxNumber);
                throw new ParserException(message);
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
                throw new ParserException(message, exception);
            }
        }
    }
}
