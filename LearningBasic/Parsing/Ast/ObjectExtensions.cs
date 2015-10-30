namespace LearningBasic.Parsing.Ast
{
    using System;
    using System.Globalization;

    public static class ObjectExtensions
    {
        public static string ToListingValue(this object o)
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
