namespace LearningBasic.Parsing.Ast
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Implements extension methods to format dynamic BASIC values in the <c>LIST</c> statement.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Formats dynamic value to print it in the <c>LIST</c> statement.
        /// </summary>
        /// <param name="o">The dynamic object.</param>
        /// <returns>The string representation of the <paramref name="o">object</paramref>.</returns>
        public static string ToPrintable(this object o)
        {
            if (o == null)
                return string.Empty;

            if (o is string)
                return '"' + (o as string).Replace("\"", "\"\"") + '"';

            if (o is IConvertible)
                return (o as IConvertible).ToString(CultureInfo.InvariantCulture);

            return o.ToString();
        }
    }
}
