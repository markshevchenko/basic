namespace Basic.Tests.Parsing
{
    using Basic.Parsing;
    using Basic.Runtime;
    using Basic.Runtime.Statements;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ScannerExtensionsTests : BaseTests
    {
        [TestMethod]
        public void ReadToken_WithExpectedToken_MovesCurrentTokenToNextToken()
        {
            var scanner = new Scanner("PRINT 123");

            scanner.ReadToken(Token.Print);

            Assert.AreEqual(Token.Integer, scanner.CurrentToken);
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void ReadToken_WithUnexpectedToken_ThrowsParserException()
        {
            var scanner = new Scanner("PRINT 123");

            scanner.ReadToken(Token.Input);
        }

        [TestMethod]
        public void ReadToken_WithExpectedTokenAndText_SetsText()
        {
            var scanner = new Scanner("  Identifier1 + Identifier2 - 3");
            string text = "initial value";

            scanner.ReadToken(Token.Identifier, out text);

            Assert.AreEqual("Identifier1", text);
        }

        [TestMethod]
        public void TryReadToken_WithExpectedToken_ReturnsTrue()
        {
            var scanner = new Scanner("PRINT 123");
            var actual = scanner.TryReadToken(Token.Print);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void TryReadToken_WithUnexpectedToken_ReturnsFalse()
        {
            var scanner = new Scanner("PRINT 123");
            var actual = scanner.TryReadToken(Token.Input);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TryReadToken_WithExpectedTokenAndText_SetsText()
        {
            var scanner = new Scanner("  Identifier1 + Identifier2 - 3");
            string text = "initial value";

            scanner.TryReadToken(Token.Identifier, out text);

            Assert.AreEqual("Identifier1", text);
        }

        [TestMethod]
        public void TryReadToken_WithUnexpectedTokenAndText_SetsTextToEmptyString()
        {
            var scanner = new Scanner("  Identifier1 + Identifier2 - 3");
            string text = "initial value";

            scanner.TryReadToken(Token.Integer, out text);

            Assert.AreEqual(string.Empty, text);
        }

        [TestMethod]
        public void TryReadToken_WithExpectedTokenAndFactory_SetsValueOfFactory()
        {
            var scanner = new Scanner("NEXT");
            IStatement value;
            scanner.TryReadToken(Token.Next, () => new Next(), out value);

            Assert.IsInstanceOfType(value, typeof(Next));
        }

        [TestMethod]
        public void TryReadToken_WithUnexpectedTokenAndFactory_SetsDefaultValue()
        {
            var scanner = new Scanner("QUIT");
            IStatement value;
            scanner.TryReadToken(Token.Next, () => new Next(), out value);

            Assert.IsNull(value);
        }
    }
}
