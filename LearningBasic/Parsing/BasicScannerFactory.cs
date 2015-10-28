namespace Basic.Parsing
{
    using System.IO;

    /// <summary>
    /// Implements BASIC scanner factory.
    /// </summary>
    public class BasicScannerFactory : IScannerFactory<Token>
    {
        /// <inheritdoc />
        public IScanner<Token> Create(TextReader inputStream)
        {
            return new BasicScanner(inputStream);
        }
    }
}
