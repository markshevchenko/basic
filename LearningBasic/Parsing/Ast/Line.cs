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

        /// <summary>
        /// Creates an instance of <see cref="Line"/> without line number.
        /// </summary>
        /// <param name="statement">The statement.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="statement"/> is <c>null</c>.
        /// </exception>
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
        /// <exception cref="ArgumentNullException">
        /// <paramref name="number"/> or
        /// <paramref name="statement"/> are <c>null</c>.
        /// </exception>
        public Line(string number, IStatement statement)
        {
            if (number == null)
                throw new ArgumentNullException("number");

            if (statement == null)
                throw new ArgumentNullException("statement");

            Number = Parse(number);
            Statement = statement;
        }

        /// <summary>
        /// Parses the number of a BASIC's line.
        /// </summary>
        /// <param name="numberAsString">The line number as a <see cref="String">string</see>.</param>
        /// <returns>The line number, or <c>null</c> if <paramref name="numberAsString"/> is null or empty.</returns>
        /// <exception cref="ParserException">
        /// The number can't be parsed, or is out of range of <see cref="MinNumber"/> to <see cref="MaxNumber"/>.
        /// </exception>
        public static int Parse(string numberAsString)
        {
            try
            {
                var number = int.Parse(numberAsString, NumberStyles.Integer, CultureInfo.InvariantCulture);

                if (number < MinNumber || number > MaxNumber)
                {
                    var message = string.Format(ErrorMessages.LineNumberOutOfRange, MinNumber, MaxNumber);
                    throw new ParserException(message);
                }

                return number;
            }
            catch (FormatException exception)
            {
                throw new ParserException(ErrorMessages.CantParseLineNumber, exception);
            }
            catch (OverflowException exception)
            {
                throw new ParserException(ErrorMessages.CantParseLineNumber, exception);
            }
        }
    }
}
