namespace LearningBasic.Parsing
{
    using System;
    using System.IO;
    using System.Text;

    public static class TextReaderExtensions
    {
        private const int Eof = -1;

        public static bool IsEof(this TextReader inputStream)
        {
            return inputStream.Peek() == Eof;
        }

        public static bool SkipIf(this TextReader inputStream, char c)
        {
            int nextCharacter = inputStream.Peek();

            if (nextCharacter == c)
            {
                inputStream.Read();

                return true;
            }

            return false;
        }

        public static void SkipWhile(this TextReader inputStream, Predicate<char> predicate)
        {
            int nextCharacter = inputStream.Peek();

            while (nextCharacter != Eof && predicate((char)nextCharacter))
            {
                inputStream.Read();

                nextCharacter = inputStream.Peek();
            }
        }

        public static bool TakeIf(this TextReader inputStream, char c, StringBuilder target)
        {
            int nextCharacter = inputStream.Peek();

            if (nextCharacter == c)
            {
                target.Append((char)nextCharacter);
                inputStream.Read();

                return true;
            }

            return false;
        }

        public static bool TakeIf(this TextReader inputStream, Predicate<char> predicate, StringBuilder target)
        {
            int nextCharacter = inputStream.Peek();

            if (nextCharacter != Eof && predicate((char)nextCharacter))
            {
                target.Append((char)nextCharacter);
                inputStream.Read();

                return true;
            }

            return false;
        }

        public static void TakeWhile(this TextReader inputStream, Predicate<char> predicate, StringBuilder target)
        {
            int nextCharacter = inputStream.Peek();

            while (nextCharacter != Eof && predicate((char)nextCharacter))
            {
                target.Append((char)nextCharacter);
                inputStream.Read();

                nextCharacter = inputStream.Peek();
            }
        }
    }
}
