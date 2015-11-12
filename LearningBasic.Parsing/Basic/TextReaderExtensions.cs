namespace LearningBasic.Parsing.Basic
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
