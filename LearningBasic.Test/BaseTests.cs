namespace Basic.Test
{
    using System.IO;
    using Basic.Parsing;

    public abstract class BaseTests
    {
        protected static TextReader MakeReader(string inputString)
        {
            return new StringReader(inputString);
        }

        protected static IScanner<Token> MakeScanner(string inputString)
        {
            var reader = MakeReader(inputString);

            return new BasicScanner(reader);
        }

        protected static IParser<Tag> MakeParser()
        {
            return new BasicParser(new BasicScannerFactory());
        }
    }
}
