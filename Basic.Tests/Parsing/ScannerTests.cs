namespace Basic.Tests.Parsing
{
    using Basic.Parsing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ScannerTests : BaseTests
    {
        [TestMethod]
        public void BasicScanner_WithWhitespaces_SetsCurrentTokenToEof()
        {
            var scanner = new Scanner("   ");
            Assert.AreEqual(Token.Eof, scanner.CurrentToken);
        }

        [TestMethod]
        public void BasicScanner_WhenConstructed_SetsCurrentTokenToFirstToken()
        {
            var scanner = new Scanner(" ( + ) ");
            Assert.AreEqual(Token.LParen, scanner.CurrentToken);
        }

        [TestMethod]
        public void MoveNext_WhenCalledOnce_SetsCurrentTokenToSecondToken()
        {
            var scanner = new Scanner(" ( + ) ");
            scanner.MoveNext();
            Assert.AreEqual(Token.Plus, scanner.CurrentToken);
        }

        [TestMethod]
        public void MoveNext_WhenCalledTwice_SetsCurrentTokenToThirdToken()
        {
            var scanner = new Scanner(" ( + ) ");

            scanner.MoveNext();
            scanner.MoveNext();

            Assert.AreEqual(Token.RParen, scanner.CurrentToken);
        }

        [TestMethod]
        public void MoveNext_WhenCalledTooManyTimes_DoesntThrowException()
        {
            var scanner = new Scanner(" ( + ) ");

            scanner.MoveNext();
            scanner.MoveNext();
            scanner.MoveNext();
            scanner.MoveNext();
            scanner.MoveNext();
        }

        [TestMethod]
        public void MoveNext_AfterRemKeyword_ReadsComment()
        {
            var scanner = new Scanner(" REM    example of a comment ");

            scanner.MoveNext();

            Assert.AreEqual(Token.Comment, scanner.CurrentToken);
        }

        [TestMethod]
        public void MoveNext_AfterRemKeyword_SkipsWhitespaces()
        {
            var scanner = new Scanner(" REM    example of a comment ");

            scanner.MoveNext();

            Assert.AreEqual("example of a comment ", scanner.CurrentText);
        }
    }
}
