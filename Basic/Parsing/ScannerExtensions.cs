namespace Basic.Parsing
{
    using System;

    /// <summary>
    /// Implements <see cref="Scanner"/> extension methods to describe primitive syntax rules.
    /// </summary>
    public static class ScannerExtensions
    {
        /// <summary>
        /// Reads specified token from the scanner.
        /// </summary>
        /// <param name="scanner">The input stream of tokens.</param>
        /// <param name="token">The next requierd token.</param>
        /// <exception cref="ParserException">The next token is different to the specified token.</exception>
        public static void ReadToken(this Scanner scanner, Token token)
        {
            if (scanner.CurrentToken == token)
            {
                scanner.MoveNext();
                return;
            }

            var message = string.Format(ErrorMessages.UnexpectedToken, token, scanner.CurrentToken);
            throw new ParserException(message);
        }

        /// <summary>
        /// Reads specified token and matched text from the scanner.
        /// </summary>
        /// <param name="scanner">The input stream of tokens.</param>
        /// <param name="token">The next requierd token.</param>
        /// <param name="text">When the method returns, contains the characters matched to the token read.</param>
        /// <exception cref="ParserException">The next token is different to the specified token.</exception>
        public static void ReadToken(this Scanner scanner, Token token, out string text)
        {
            if (scanner.CurrentToken == token)
            {
                text = scanner.CurrentText;
                scanner.MoveNext();
                return;
            }

            var message = string.Format(ErrorMessages.UnexpectedToken, token, scanner.CurrentToken);
            throw new ParserException(message);
        }

        /// <summary>
        /// Reads specified token from the scanner.
        /// A return value indicating whether the read succeeded.
        /// </summary>
        /// <param name="scanner">The input stream of tokens.</param>
        /// <param name="token">The next requierd token.</param>
        /// <returns><c>true</c> if the next token is equals to <paramref name="token"/>; otherwise, <c>false</c>.</returns>
        public static bool TryReadToken(this Scanner scanner, Token token)
        {
            if (scanner.CurrentToken == token)
            {
                scanner.MoveNext();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Reads specified token and matched text from the scanner.
        /// A return value indicating whether the read succeeded.
        /// </summary>
        /// <param name="scanner">The input stream of tokens.</param>
        /// <param name="token">The next requierd token.</param>
        /// <param name="text">
        /// When the method returns, contains the characters matched to the token read,
        /// or <c>null</c>, if the read failed.
        /// </param>
        /// <returns><c>true</c> if the next token is equals to <paramref name="token"/>; otherwise, <c>false</c>.</returns>
        public static bool TryReadToken(this Scanner scanner, Token token, out string text)
        {
            if (scanner.CurrentToken == token)
            {
                text = scanner.CurrentText;
                scanner.MoveNext();
                return true;
            }

            text = string.Empty;
            return false;
        }

        /// <summary>
        /// Reads specified token from the scanner and creates matched result.
        /// A return value indicating whether the read succeeded.
        /// </summary>
        /// <param name="scanner">The input stream of tokens.</param>
        /// <param name="token">The next requierd token.</param>
        /// <param name="factory">The factory to create result.</param>
        /// <param name="result">
        /// When the method returns, contains the result created with the <paramref name="factory"/>,
        /// or <c>default(TResult)</c>, if the read failed.
        /// </param>
        /// <returns><c>true</c> if the next token is equals to <paramref name="token"/>; otherwise, <c>false</c>.</returns>
        /// <remarks>This method is useful for parsing simplest syntax constructs like keywords without any parameters.</remarks>
        public static bool TryReadToken<TResult>(this Scanner scanner, Token token, Func<TResult> factory, out TResult result)
        {
            if (scanner.CurrentToken == token)
            {
                scanner.MoveNext();
                result = factory();
                return true;
            }

            result = default;
            return false;
        }
    }
}
