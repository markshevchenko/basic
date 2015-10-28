namespace LearningBasic.Parsing
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Implements scanning helper extension methods.
    /// </summary>
    public static class ScannerExtensions
    {
        /// <summary>
        /// Reads specified token from input stream.
        /// </summary>
        /// <typeparam name="TToken">The type of the token.</typeparam>
        /// <param name="scanner">The scanner to read tokens.</param>
        /// <param name="token">The next expected token.</param>
        /// <exception cref="UnexpectedTokenException">The next token is different to the specified token.</exception>
        public static void ReadToken<TToken>(this IScanner<TToken> scanner, TToken token)
            where TToken : struct
        {
            if (EqualityComparer<TToken>.Default.Equals(scanner.CurrentToken, token))
            {
                scanner.MoveNext();
                return;
            }

            throw new UnexpectedTokenException(token, scanner.CurrentToken);
        }

        public static void ReadToken<TToken>(this IScanner<TToken> scanner, TToken token, out string text)
            where TToken : struct
        {
            if (EqualityComparer<TToken>.Default.Equals(scanner.CurrentToken, token))
            {
                text = scanner.CurrentText;
                scanner.MoveNext();
                return;
            }

            throw new UnexpectedTokenException(token, scanner.CurrentToken);
        }

        public static bool TryReadToken<TToken>(this IScanner<TToken> scanner, TToken token)
            where TToken : struct
        {
            if (EqualityComparer<TToken>.Default.Equals(scanner.CurrentToken, token))
            {
                scanner.MoveNext();
                return true;
            }

            return false;
        }

        public static bool TryReadToken<TToken>(this IScanner<TToken> scanner, TToken token, out string text)
            where TToken : struct
        {
            if (EqualityComparer<TToken>.Default.Equals(scanner.CurrentToken, token))
            {
                text = scanner.CurrentText;
                scanner.MoveNext();
                return true;
            }

            text = string.Empty;
            return false;
        }

        public static AstNode<TTag> ReadNode<TToken, TTag>(this IScanner<TToken> scanner, Func<IScanner<TToken>, AstNode<TTag>> parser, string errorFormat, params object[] errorArgs)
            where TToken : struct
            where TTag : struct
        {
            var result = parser(scanner);

            if (result == null)
                throw new ParserException(string.Format(errorFormat, errorArgs));

            return result;
        }

        public static bool TryReadNode<TToken, TTag>(this IScanner<TToken> scanner, Func<IScanner<TToken>, AstNode<TTag>> parser, out AstNode<TTag> node)
            where TToken : struct
            where TTag : struct
        {
            node = parser(scanner);

            return node != null;
        }
    }
}
