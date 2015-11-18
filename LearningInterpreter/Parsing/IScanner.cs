namespace LearningInterpreter.Parsing
{
    using System;

    /// <summary>
    /// Declares the interface of the lexical analyzer (scanner).
    /// </summary>
    /// <typeparam name="TToken">The type of token.</typeparam>
    /// <remarks>The scanner is an input stream of tokens.</remarks>
    public interface IScanner<TToken> : IDisposable
        where TToken : struct
    {
        /// <summary>
        /// Gets last read token.
        /// </summary>
        /// <remarks><c>default(<typeparam name="TToken"/>)</c>, if there isn't tokens to read.</remarks>
        TToken CurrentToken { get; }

        /// <summary>
        /// Gets last read token's text.
        /// </summary>
        string CurrentText { get; }

        /// <summary>
        /// Reads next token from the input stream, and sets <see cref="CurrentToken"/> and <see cref="CurrentText"/> properties.
        /// </summary>
        /// <remarks>
        /// If the end of stream found, the method sets <see cref="CurrentToken"/> to <c>default(<typeparam name="TToken"/>)</c>
        /// value.
        /// </remarks>
        void MoveNext();
    }
}
