namespace Basic.Parsing
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Implements <see cref="TextReader"/> extension methods to describe BASIC-specific lexical rules.
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

        public static bool TryReadPunctuationMark(this TextReader inputStream, StringBuilder text, out Token token)
        {
            return TryReadDictionary(inputStream, text, punctuationMarks, out token);
        }

        private static readonly IDictionary<char, Token> punctuationMarks = new Dictionary<char, Token>
        {
            { '(', Token.LParen },
            { ')', Token.RParen },
            { '[', Token.LBracket },
            { ']', Token.RBracket },
            { ';', Token.Semicolon },
            { ',', Token.Comma },
        };

        private static bool TryReadDictionary(TextReader inputStream, StringBuilder text, IDictionary<char, Token> dictionary, out Token token)
        {
            foreach (var charToken in dictionary)
            {
                if (inputStream.TakeIf(charToken.Key, text))
                {
                    token = charToken.Value;
                    return true;
                }
            }

            token = Token.Unknown;
            return false;
        }

        public static bool TryReadOperator(this TextReader inputStream, StringBuilder text, out Token token)
        {
            if (TryReadDictionary(inputStream, text, singleCharacterOperators, out token))
                return true;

            token = Token.Unknown;

            if (inputStream.TakeIf('<', text))
            {
                if (inputStream.TakeIf('=', text))
                    token = Token.Le;
                else if (inputStream.TakeIf('>', text))
                    token = Token.Ne;
                else
                    token = Token.Lt;
            }
            else if (inputStream.TakeIf('>', text))
            {
                if (inputStream.TakeIf('=', text))
                    token = Token.Ge;
                else
                    token = Token.Gt;
            }

            return token != Token.Unknown;
        }

        private static readonly IDictionary<char, Token> singleCharacterOperators = new Dictionary<char, Token>
        {
            { '+', Token.Plus },
            { '-', Token.Minus },
            { '*', Token.Asterisk },
            { '/', Token.Slash },
            { '^', Token.Caret },
            { '=', Token.Eq },
        };

        public static bool TryReadString(this TextReader inputStream, StringBuilder text, out Token token)
        {
            token = Token.Unknown;

            if (inputStream.SkipIf('"'))
            {
                do
                {
                    inputStream.TakeWhile(c => c != '"', text);

                    if (!inputStream.SkipIf('"'))
                        throw new ParserException(ErrorMessages.UnexpectedEndOfFile);
                }
                while (inputStream.TakeIf('"', text));

                token = Token.String;
            }

            return token != Token.Unknown;
        }

        public static bool TryReadIntegerOrFloatNumber(this TextReader inputStream, StringBuilder text, out Token token)
        {
            token = Token.Unknown;

            if (inputStream.TakeIf(char.IsDigit, text))
            {
                inputStream.TakeWhile(char.IsDigit, text);

                if (inputStream.TakeIf('.', text))
                {
                    if (!inputStream.TakeIf(char.IsDigit, text))
                    {
                        var nextCharacter = (char)inputStream.Peek();
                        var message = string.Format(ErrorMessages.UnexpectedCharacter, nextCharacter);
                        throw new ParserException(message);
                    }

                    inputStream.TakeWhile(char.IsDigit, text);

                    token = Token.Float;
                }
                else
                    token = Token.Integer;
            }

            return token != Token.Unknown;
        }

        public static bool TryReadIdentifierOrKeyword(this TextReader inputStream, StringBuilder text, out Token token)
        {
            if (inputStream.TakeIf(c => c == '_' || char.IsLetter(c), text))
            {
                inputStream.TakeWhile(c => c == '_' || char.IsLetter(c) || char.IsDigit(c), text);

                if (!keywords.TryGetValue(text.ToString(), out token))
                    token = Token.Identifier;

                return true;
            }

            token = Token.Unknown;
            return false;
        }

        private static readonly IDictionary<string, Token> keywords = new SortedDictionary<string, Token>(StringComparer.OrdinalIgnoreCase)
        {
            { "and", Token.And },
            { "dim", Token.Dim },
            { "else", Token.Else },
            { "end", Token.End },
            { "for", Token.For },
            { "goto", Token.Goto },
            { "if", Token.If },
            { "input", Token.Input },
            { "let", Token.Let },
            { "list", Token.List },
            { "load", Token.Load },
            { "mod", Token.Mod },
            { "next", Token.Next },
            { "not", Token.Not },
            { "or", Token.Or },
            { "print", Token.Print },
            { "quit", Token.Quit },
            { "randomize", Token.Randomize },
            { "rem", Token.Rem },
            { "remove", Token.Remove },
            { "run", Token.Run },
            { "save", Token.Save },
            { "step", Token.Step },
            { "then", Token.Then },
            { "to", Token.To },
            { "xor", Token.Xor },
        };
    }
}
