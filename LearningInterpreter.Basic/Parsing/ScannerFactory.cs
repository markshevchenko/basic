namespace LearningInterpreter.Basic.Parsing
{
    using System.IO;
    using LearningInterpreter.Parsing;

    /// <summary>
    /// Implements BASIC-specific scanner factory.
    /// </summary>
    public class ScannerFactory : IScannerFactory<Token>
    {
        /// <inheritdoc />
        public IScanner<Token> Create(TextReader inputStream)
        {
            return new Scanner(inputStream);
        }
    }
}
