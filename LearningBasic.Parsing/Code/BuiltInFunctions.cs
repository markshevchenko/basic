namespace LearningBasic.Parsing.Code
{
    using System;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Implements built-in BASIC functions.
    /// </summary>
    public static class BuiltInFunctions
    {
        internal const string TestFunctionName = "_test";
        internal const int TestExpected = 100;

        public const string DefaultDelimiter = " ";

        public static dynamic LEN(string s)
        {
            return s.Length;
        }

        public static dynamic LEN(object[] array)
        {
            return array.Length;
        }

        public static dynamic STR(string s)
        {
            return s;
        }

        public static dynamic STR(int i)
        {
            return i.ToString(CultureInfo.InvariantCulture);
        }

        public static dynamic STR(double d)
        {
            return d.ToString(CultureInfo.InvariantCulture);
        }

        public static dynamic CHR(int i)
        {
            return Char.ConvertFromUtf32(i);
        }

        public static dynamic ASC(string s)
        {
            if (s == string.Empty)
                return 0;

            return Char.ConvertToUtf32(s, 0);
        }

        public static dynamic MID(string s, int start)
        {
            return s.Substring(start - 1);
        }

        public static dynamic MID(string s, int start, int length)
        {
            return s.Substring(start - 1, length);
        }

        public static dynamic UPPER(string s)
        {
            return s.ToUpper();
        }

        public static dynamic LOWER(string s)
        {
            return s.ToLower();
        }

        public static dynamic TRIM(string s)
        {
            return s.Trim();
        }

        public static dynamic INSTR(string s1, string s2)
        {
            return 1 + s1.IndexOf(s2);
        }

        public static dynamic INSTR(int start, string s1, string s2)
        {
            return 1 + s1.IndexOf(s2, start - 1);
        }

        public static dynamic INSTRREV(string s1, string s2)
        {
            return 1 + s1.LastIndexOf(s2);
        }

        public static dynamic INSTRREV(int start, string s1, string s2)
        {
            return 1 + s1.LastIndexOf(s2, start - 1);
        }

        public static dynamic REPLACE(string s, string find, string replacement)
        {
            return s.Replace(find, replacement);
        }

        public static dynamic JOIN(object[] array)
        {
            return string.Join(DefaultDelimiter, array);
        }

        public static dynamic JOIN(object[] array, string delimiter)
        {
            return string.Join(delimiter, array);
        }

        public static dynamic SPLIT(string s)
        {
            return s.Split(new[] { DefaultDelimiter }, StringSplitOptions.None);
        }

        public static dynamic SPLIT(string s, string delimiter)
        {
            return s.Split(new[] { delimiter }, StringSplitOptions.None);
        }

        public static dynamic ABS(int i)
        {
            return Math.Abs(i);
        }

        public static dynamic ABS(double d)
        {
            return Math.Abs(d);
        }

        public static dynamic SIGN(int i)
        {
            return Math.Sign(i);
        }

        public static dynamic SIGN(double d)
        {
            return Math.Sign(d);
        }

        public static dynamic MAX(int i1, int i2)
        {
            return Math.Max(i1, i2);
        }

        public static dynamic MAX(double d1, double d2)
        {
            return Math.Max(d1, d2);
        }

        public static dynamic MAX(string s1, string s2)
        {
            return s1.CompareTo(s2) > 0 ? s1 : s2;
        }

        public static dynamic MAX(object[] array)
        {
            return array.Max();
        }

        public static dynamic MIN(int i1, int i2)
        {
            return Math.Min(i1, i2);
        }

        public static dynamic MIN(double d1, double d2)
        {
            return Math.Min(d1, d2);
        }

        public static dynamic MIN(string s1, string s2)
        {
            return s1.CompareTo(s2) < 0 ? s1 : s2;
        }

        public static dynamic MIN(object[] array)
        {
            return array.Min();
        }

        public static dynamic EXP(double d)
        {
            return Math.Exp(d);
        }

        public static dynamic LN(double d)
        {
            return Math.Log(d);
        }

        public static dynamic LOG(double d)
        {
            return Math.Log10(d);
        }

        public static dynamic SQRT(double d)
        {
            return Math.Sqrt(d);
        }

        public static dynamic SIN(double d)
        {
            return Math.Sin(d);
        }

        public static dynamic COS(double d)
        {
            return Math.Cos(d);
        }

        public static dynamic TAN(double d)
        {
            return Math.Tan(d);
        }

        public static dynamic ASIN(double d)
        {
            return Math.Asin(d);
        }

        public static dynamic ACOS(double d)
        {
            return Math.Acos(d);
        }

        public static dynamic ATAN(double d)
        {
            return Math.Atan(d);
        }

        public static dynamic ATAN2(double y, double x)
        {
            return Math.Atan2(y, x);
        }

        public static dynamic CEIL(int i)
        {
            return i;
        }

        public static dynamic CEIL(double d)
        {
            return Math.Ceiling(d);
        }

        public static dynamic FLOOR(int i)
        {
            return i;
        }

        public static dynamic FLOOR(double d)
        {
            return Math.Floor(d);
        }

        public static dynamic _TEST()
        {
            return TestExpected;
        }

        public static dynamic _TEST(int i)
        {
            return TestExpected;
        }

        public static dynamic _TEST(int i, int j)
        {
            return TestExpected;
        }

        public static dynamic _TEST(int i, int j, int k)
        {
            return TestExpected;
        }
    }
}
