namespace LearningBasic.Parsing
{
    using LearningBasic.Parsing.Basic;
    using LearningBasic.Parsing.Code.Statements;
    using LearningBasic.RunTime;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ScannerExtensionsTests : BaseTests
    {
        [TestMethod]
        public void ReadToken_WithExpectedToken_MovesCurrentTokenToNextToken()
        {
            using (var scanner = MakeScanner("PRINT 123"))
            {
                scanner.ReadToken(Token.Print);

                Assert.AreEqual(Token.Integer, scanner.CurrentToken);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void ReadToken_WithUnexpectedToken_ThrowsParserException()
        {
            using (var scanner = MakeScanner("PRINT 123"))
            {
                scanner.ReadToken(Token.Input);
            }
        }

        [TestMethod]
        public void ReadToken_WithExpectedTokenAndText_SetsText()
        {
            using (var scanner = MakeScanner("  Identifier1 + Identifier2 - 3"))
            {
                string text = "initial value";

                scanner.ReadToken(Token.Identifier, out text);

                Assert.AreEqual("Identifier1", text);
            }
        }

        [TestMethod]
        public void TryReadToken_WithExpectedToken_ReturnsTrue()
        {
            using (var scanner = MakeScanner("PRINT 123"))
            {
                var actual = scanner.TryReadToken(Token.Print);

                Assert.IsTrue(actual);
            }
        }

        [TestMethod]
        public void TryReadToken_WithUnexpectedToken_ReturnsFalse()
        {
            using (var scanner = MakeScanner("PRINT 123"))
            {
                var actual = scanner.TryReadToken(Token.Input);

                Assert.IsFalse(actual);
            }
        }

        [TestMethod]
        public void TryReadToken_WithExpectedTokenAndText_SetsText()
        {
            using (var scanner = MakeScanner("  Identifier1 + Identifier2 - 3"))
            {
                string text = "initial value";

                scanner.TryReadToken(Token.Identifier, out text);

                Assert.AreEqual("Identifier1", text);
            }
        }

        [TestMethod]
        public void TryReadToken_WithUnexpectedTokenAndText_SetsTextToEmptyString()
        {
            using (var scanner = MakeScanner("  Identifier1 + Identifier2 - 3"))
            {
                string text = "initial value";

                scanner.TryReadToken(Token.Integer, out text);

                Assert.AreEqual(string.Empty, text);
            }
        }

        [TestMethod]
        public void TryReadToken_WithExpectedTokenAndFactory_SetsValueOfFactory()
        {
            using (var scanner = MakeScanner("NEXT"))
            {
                IStatement value;
                scanner.TryReadToken(Token.Next, () => new Next(), out value);

                Assert.IsInstanceOfType(value, typeof(Next));
            }
        }

        [TestMethod]
        public void TryReadToken_WithUnexpectedTokenAndFactory_SetsDefaultValue()
        {
            using (var scanner = MakeScanner("QUIT"))
            {
                IStatement value;
                scanner.TryReadToken(Token.Next, () => new Next(), out value);

                Assert.IsNull(value);
            }
        }
    }
}
