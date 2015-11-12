namespace LearningBasic.Parsing
{
    using System;
    using System.Globalization;
    using LearningBasic.RunTime;

    /// <summary>
    /// Represents the BASIC line that is a statement with a possible line number.
    /// </summary>
    public class Line : ILine, IComparable, IComparable<Line>
    {
        public const int MinNumber = 1;
        public const int MaxNumber = 99999;

        /// <inheritdoc />
        public virtual string Label { get; private set; }

        /// <summary>
        /// Gets the line number or <c>null</c> if it isn't present.
        /// </summary>
        public int? Number { get; private set; }

        /// <inheritdoc />
        public virtual IStatement Statement { get; private set; }

        /// <summary>
        /// Initializes an instance of <see cref="Line"/> class with the specified statement.
        /// </summary>
        /// <param name="statement">The statement.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="statement"/> is <c>null</c>.
        /// </exception>
        public Line(IStatement statement)
        {
            if (statement == null)
                throw new ArgumentNullException("statement");

            Label = null;
            Number = null;
            Statement = statement;
        }

        /// <summary>
        /// Initializes an instance of <see cref="Line"/> with the specified label and statement.
        /// </summary>
        /// <param name="label">
        /// The label of the line, i.e. string representation of the number of the line.
        /// </param>
        /// <param name="statement">The statement.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="label"/> or
        /// <paramref name="statement"/> are <c>null</c>.
        /// </exception>
        public Line(string label, IStatement statement)
        {
            if (label == null)
                throw new ArgumentNullException("number");

            if (statement == null)
                throw new ArgumentNullException("statement");

            Label = label;
            Number = Parse(label);
            Statement = statement;
        }

        /// <summary>
        /// Initializes an instance of <see cref="Line"/> with the specified number and statement.
        /// </summary>
        /// <param name="number">The number of the line.</param>
        /// <param name="statement">The statement.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="statement"/> is <c>null</c>.
        /// </exception>
        public Line(int number, IStatement statement)
        {
            if (statement == null)
                throw new ArgumentNullException("statement");

            Label = number.ToString(CultureInfo.InvariantCulture);
            Number = number;
            Statement = statement;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Number.HasValue
                   ? Number.Value.ToString(CultureInfo.InvariantCulture) + " " + Statement
                   : Statement.ToString();
        }

        /// <summary>
        /// Parses a string representation of a line number and checks if it's valid.
        /// </summary>
        /// <param name="label">The string representation of a line number.</param>
        /// <returns>The line number.</returns>
        /// <exception cref="ParserException">Something went wrong.</exception>
        internal static int Parse(string label)
        {
            try
            {
                var number = int.Parse(label, NumberStyles.None, CultureInfo.InvariantCulture);

                if (number >= MinNumber && number <= MaxNumber)
                    return number;

                var message = string.Format(ErrorMessages.LineNumberOutOfRange, MinNumber, MaxNumber);
                throw new ParserException(message);
            }
            catch (FormatException exception)
            {
                var message = string.Format(ErrorMessages.CantParseLineNumber, label);
                throw new ParserException(message, exception);
            }
            catch (OverflowException exception)
            {
                var message = string.Format(ErrorMessages.LineNumberOutOfRange, MinNumber, MaxNumber);
                throw new ParserException(message, exception);
            }
        }

        /// <inheritdoc />
        public int CompareTo(Line other)
        {
            if (other == null)
                return 1;

            if (Number.HasValue)
            {
                if (other.Number.HasValue)
                    return Number.Value - other.Number.Value;

                return 1;
            }

            if (other.Number.HasValue)
                return -1;

            return 0;
        }

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            var line = obj as Line;

            if (line == null)
                return -1;

            return CompareTo(line);
        }
    }
}
