namespace LearningBasic.Parsing
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Implements <see cref="TextReader"/> extension methods to describe primitive lexical rules.
    /// </summary>
    public static class TextReaderExtensions
    {
        private const int Eof = -1;

        /// <summary>
        /// Determines whether the end of the reader is reached.
        /// </summary>
        /// <param name="reader">The text reader.</param>
        /// <returns><c>true</c> after the end of input <paramref name="reader"/> is reached; otherwise, <c>false</c>.</returns>
        public static bool IsEof(this TextReader reader)
        {
            return reader.Peek() == Eof;
        }

        /// <summary>
        /// Skips the next character in the reader, if it matches with the specified character.
        /// </summary>
        /// <param name="reader">The text reader.</param>
        /// <param name="c">The character to comparison.</param>
        /// <returns><c>true</c> if the character is matched; otherwise, <c>false</c>.</returns>
        public static bool SkipIf(this TextReader reader, char c)
        {
            int nextCharacter = reader.Peek();

            if (nextCharacter == c)
            {
                reader.Read();

                return true;
            }

            return false;
        }

        /// <summary>
        /// Skips characters in the reader as long as the specified codition is <c>true</c>.
        /// </summary>
        /// <param name="reader">The text reader.</param>
        /// <param name="predicate">The function to test each character for a condition.</param>
        public static void SkipWhile(this TextReader reader, Predicate<char> predicate)
        {
            int nextCharacter = reader.Peek();

            while (nextCharacter != Eof && predicate((char)nextCharacter))
            {
                reader.Read();

                nextCharacter = reader.Peek();
            }
        }

        /// <summary>
        /// Reads the next character in the reader and appends it to the string builder,
        /// if it matches with the specified character.
        /// </summary>
        /// <param name="reader">The text reader.</param>
        /// <param name="c">The character to comparison.</param>
        /// <param name="builder">The string builder.</param>
        /// <returns><c>true</c> if the character is matched; otherwise, <c>false</c>.</returns>
        public static bool TakeIf(this TextReader reader, char c, StringBuilder builder)
        {
            int nextCharacter = reader.Peek();

            if (nextCharacter == c)
            {
                builder.Append((char)nextCharacter);
                reader.Read();

                return true;
            }

            return false;
        }

        /// <summary>
        /// Reads the next character in the reader and appends it to the string builder,
        /// if the specified codition is <c>true</c>.
        /// </summary>
        /// <param name="reader">The text reader.</param>
        /// <param name="predicate">The function to test next character for a condition.</param>
        /// <param name="builder">The string builder.</param>
        /// <returns><c>true</c> if the character is matched; otherwise, <c>false</c>.</returns>
        public static bool TakeIf(this TextReader reader, Predicate<char> predicate, StringBuilder builder)
        {
            int nextCharacter = reader.Peek();

            if (nextCharacter != Eof && predicate((char)nextCharacter))
            {
                builder.Append((char)nextCharacter);
                reader.Read();

                return true;
            }

            return false;
        }

        /// <summary>
        /// Reads characters in the reader and appends them to the string builder,
        /// as long as the specified codition is <c>true</c>.
        /// </summary>
        /// <param name="reader">The text reader.</param>
        /// <param name="predicate">The function to test next character for a condition.</param>
        /// <param name="builder">The string builder.</param>
        public static void TakeWhile(this TextReader reader, Predicate<char> predicate, StringBuilder builder)
        {
            int nextCharacter = reader.Peek();

            while (nextCharacter != Eof && predicate((char)nextCharacter))
            {
                builder.Append((char)nextCharacter);
                reader.Read();

                nextCharacter = reader.Peek();
            }
        }
    }
}
