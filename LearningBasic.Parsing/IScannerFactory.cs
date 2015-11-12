namespace LearningBasic.Parsing
{
    using System.IO;

    /// <summary>
    /// Declares the interface of the scanner factory.
    /// </summary>
    /// <remarks>
    /// The factory is used by the <see cref="ILineParser"/> to create
    /// new <see cref="IScanner{TToken}"/> for each input line.
    /// </remarks>
    public interface IScannerFactory<TToken>
        where TToken : struct
    {
        /// <summary>
        /// Creates the scanner that will scan the specified <paramref name="inputStream">input stream</paramref>.
        /// </summary>
        /// <param name="inputStream">The text input stream.</param>
        /// <returns>The scanner.</returns>
        IScanner<TToken> Create(TextReader inputStream);
    }
}
