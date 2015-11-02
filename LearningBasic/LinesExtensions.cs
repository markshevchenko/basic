namespace LearningBasic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class LinesExtensions
    {
        /// <summary>
        /// Converts an enumerable of lines to the printable form.
        /// </summary>
        /// <param name="lines">The enumeration of pairs those key is a line number and value is a statement.</param>
        /// <returns>The enumeration of string each of which represents a line in a printable form.</returns>
        public static IEnumerable<string> ToPrintable(this IEnumerable<KeyValuePair<int, IStatement>> lines)
        {
            if (lines == null)
                throw new ArgumentNullException("lines");

            if (lines.Any())
            {
                var format = GetFormat(lines);

                return lines.Select(pair => new { Number = pair.Key, Statement = pair.Value })
                            .Select(line => string.Format(format, line.Number, line.Statement));
            }

            return Enumerable.Empty<string>();
        }

        /// <summary>
        /// Gets string to <see cref="string.Format">format</see> each of <paramref name="lines"/>.
        /// </summary>
        /// <param name="lines">The lines to conversion to string form.</param>
        /// <returns>The format string with two placeholders line <c>"{0,2} {1}"</c>.</returns>
        /// <remarks>The 0-th placeholders is used to format a line nubmer,
        /// and the 1-st placeholder is used to format a statement.</remarks>
        private static string GetFormat(this IEnumerable<KeyValuePair<int, IStatement>> lines)
        {
            var maxNumber = lines.Max(line => line.Key);

            return GetEnoughWideFormat(maxNumber) + " {1}";
        }

        /// <summary>
        /// Gets format string that enough wide to accomodate specific <paramref name="number"/>.
        /// </summary>
        /// <param name="number">The integer number.</param>
        /// <returns>The format string. F.e. a 3-digit number produces the string <c>"{0,3}"</c>.</returns>
        public static string GetEnoughWideFormat(int number)
        {
            if (number < 10)
                return "{0,1}";
            else if (number < 100)
                return "{0,2}";
            else if (number < 1000)
                return "{0,3}";
            else if (number < 10000)
                return "{0,4}";
            else
                return "{0,5}";
        }
    }
}
