namespace Basic
{
    using System.IO;

    /// <summary>
    /// Describes an interface for scanner factory.
    /// </summary>
    public interface IScannerFactory<TToken>
        where TToken : struct
    {
        /// <summary>
        /// Creates the scanner for the <paramref name="inputStream">input stream</paramref>.
        /// </summary>
        /// <param name="inputStream">The input stream.</param>
        /// <returns>The scanner.</returns>
        IScanner<TToken> Create(TextReader inputStream);
    }
}
