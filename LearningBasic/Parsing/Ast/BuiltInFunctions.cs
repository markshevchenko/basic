namespace LearningBasic.Parsing.Ast
{
    using System;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Implements built-in BASIC functions.
    /// </summary>
    public static class BuiltInFunctions
    {
        public const string DefaultDelimiter = " ";

        public static dynamic Len(string s)
        {
            return s.Length;
        }

        public static dynamic Str(string s)
        {
            return s;
        }

        public static dynamic Str(int i)
        {
            return i.ToString(CultureInfo.InvariantCulture);
        }

        public static dynamic Str(double d)
        {
            return d.ToString(CultureInfo.InvariantCulture);
        }

        public static dynamic Chr(int i)
        {
            return Char.ConvertFromUtf32(i);
        }

        public static dynamic Asc(string s)
        {
            if (s == string.Empty)
                return 0;

            return Char.ConvertToUtf32(s, 0);
        }

        public static dynamic Mid(string s, int start)
        {
            return s.Substring(1 + start);
        }

        public static dynamic Mid(string s, int start, int length)
        {
            return s.Substring(1 + start, length);
        }

        public static dynamic Upper(string s)
        {
            return s.ToUpper();
        }

        public static dynamic Lower(string s)
        {
            return s.ToLower();
        }

        public static dynamic Trim(string s)
        {
            return s.Trim();
        }

        public static dynamic InStr(string s1, string s2)
        {
            return 1 + s1.IndexOf(s2);
        }

        public static dynamic InStr(int start, string s1, string s2)
        {
            return 1 + s1.IndexOf(s2, 1 + start);
        }

        public static dynamic InStrRev(string s1, string s2)
        {
            return 1 + s1.LastIndexOf(s2);
        }

        public static dynamic InStrRev(int start, string s1, string s2)
        {
            return 1 + s1.LastIndexOf(s2, 1 + start);
        }

        public static dynamic Replace(string s, string find, string replacement)
        {
            return s.Replace(find, replacement);
        }

        public static dynamic Join(string[] array)
        {
            return string.Join(DefaultDelimiter, array);
        }

        public static dynamic Join(string[] array, string delimiter)
        {
            return string.Join(delimiter, array);
        }

        public static dynamic Split(string s)
        {
            return s.Split(new[] { DefaultDelimiter }, StringSplitOptions.None);
        }

        public static dynamic Split(string s, string delimiter)
        {
            return s.Split(new[] { delimiter }, StringSplitOptions.None);
        }

        public static dynamic Abs(int i)
        {
            return Math.Abs(i);
        }

        public static dynamic Abs(double d)
        {
            return Math.Abs(d);
        }

        public static dynamic Sign(int i)
        {
            return Math.Sign(i);
        }

        public static dynamic Sign(double d)
        {
            return Math.Sign(d);
        }

        public static dynamic Max(int i1, int i2)
        {
            return Math.Max(i1, i2);
        }

        public static dynamic Max(int i1, double d2)
        {
            if (i1 >= d2)
                return i1;

            return d2;
        }

        public static dynamic Max(double d1, int i2)
        {
            if (i2 >= d1)
                return i2;

            return d1;
        }

        public static dynamic Max(double d1, double d2)
        {
            return Math.Max(d1, d2);
        }

        public static dynamic Max(string s1, string s2)
        {
            return s1.CompareTo(s2) > 0 ? s1 : s2;
        }

        public static dynamic Max(int[] array)
        {
            return array.Max();
        }

        public static dynamic Max(double[] array)
        {
            return array.Max();
        }

        public static dynamic Max(string[] array)
        {
            return array.Max();
        }

        public static dynamic Min(int i1, int i2)
        {
            return Math.Min(i1, i2);
        }

        public static dynamic Min(int i1, double d2)
        {
            if (i1 <= d2)
                return i1;

            return d2;
        }

        public static dynamic Min(double d1, int i2)
        {
            if (i2 <= d1)
                return i2;

            return d1;
        }

        public static dynamic Min(double d1, double d2)
        {
            return Math.Min(d1, d2);
        }

        public static dynamic Min(string s1, string s2)
        {
            return s1.CompareTo(s2) < 0 ? s1 : s2;
        }

        public static dynamic Min(int[] array)
        {
            return array.Min();
        }

        public static dynamic Min(double[] array)
        {
            return array.Min();
        }

        public static dynamic Min(string[] array)
        {
            return array.Min();
        }

        public static dynamic Avg(int[] array)
        {
            return array.Average();
        }

        public static dynamic Avg(double[] array)
        {
            return array.Average();
        }

        public static dynamic Exp(int i)
        {
            return Math.Exp(i);
        }

        public static dynamic Exp(double d)
        {
            return Math.Exp(d);
        }

        public static dynamic Ln(int i)
        {
            return Math.Log(i);
        }

        public static dynamic Ln(double d)
        {
            return Math.Log(d);
        }

        public static dynamic Log(int i)
        {
            return Math.Log10(i);
        }

        public static dynamic Log(double d)
        {
            return Math.Log10(d);
        }

        public static dynamic Sqrt(int i)
        {
            return Math.Sqrt(i);
        }

        public static dynamic Sqrt(double d)
        {
            return Math.Sqrt(d);
        }

        public static dynamic Sin(int i)
        {
            return Math.Sin(i);
        }

        public static dynamic Sin(double d)
        {
            return Math.Sin(d);
        }

        public static dynamic Cos(int i)
        {
            return Math.Cos(i);
        }

        public static dynamic Cos(double d)
        {
            return Math.Cos(d);
        }

        public static dynamic Tan(int i)
        {
            return Math.Tan(i);
        }

        public static dynamic Tan(double d)
        {
            return Math.Tan(d);
        }

        public static dynamic Asin(int i)
        {
            return Math.Asin(i);
        }

        public static dynamic Asin(double d)
        {
            return Math.Asin(d);
        }

        public static dynamic Acos(int i)
        {
            return Math.Acos(i);
        }

        public static dynamic Acos(double d)
        {
            return Math.Acos(d);
        }

        public static dynamic Atan(int i)
        {
            return Math.Atan(i);
        }

        public static dynamic Atan(double d)
        {
            return Math.Atan(d);
        }

        public static dynamic Atan2(int y, int x)
        {
            return Math.Atan2(y, x);
        }

        public static dynamic Atan2(int y, double x)
        {
            return Math.Atan2(y, x);
        }

        public static dynamic Atan2(double y, int x)
        {
            return Math.Atan2(y, x);
        }

        public static dynamic Atan2(double y, double x)
        {
            return Math.Atan2(y, x);
        }

        public static dynamic Ceil(int i)
        {
            return i;
        }

        public static dynamic Ceil(double d)
        {
            return Math.Ceiling(d);
        }

        public static dynamic Floor(int i)
        {
            return i;
        }

        public static dynamic Floor(double d)
        {
            return Math.Floor(d);
        }
    }
}
