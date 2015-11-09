namespace LearningBasic.Parsing
{
    using LearningBasic.Parsing.Basic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BasicScannerTests : BaseTests
    {
        [TestMethod]
        public void BasicScanner_WithWhitespaces_SetsCurrentTokenToEof()
        {
            using (var scanner = MakeScanner("   "))
            {
                Assert.AreEqual(Token.Eof, scanner.CurrentToken);
            }
        }

        [TestMethod]
        public void BasicScanner_WhenConstructed_SetsCurrentTokenToFirstToken()
        {
            using (var scanner = MakeScanner(" ( + ) "))
            {
                Assert.AreEqual(Token.LParen, scanner.CurrentToken);
            }
        }

        [TestMethod]
        public void MoveNext_WhenCalledOnce_SetsCurrentTokenToSecondToken()
        {
            using (var scanner = MakeScanner(" ( + ) "))
            {
                scanner.MoveNext();

                Assert.AreEqual(Token.Plus, scanner.CurrentToken);
            }
        }

        [TestMethod]
        public void MoveNext_WhenCalledTwice_SetsCurrentTokenToThirdToken()
        {
            using (var scanner = MakeScanner(" ( + ) "))
            {
                scanner.MoveNext();
                scanner.MoveNext();

                Assert.AreEqual(Token.RParen, scanner.CurrentToken);
            }
        }

        [TestMethod]
        public void MoveNext_WhenCalledTooManyTimes_DoesntThrowException()
        {
            using (var scanner = MakeScanner(" ( + ) "))
            {
                scanner.MoveNext();
                scanner.MoveNext();
                scanner.MoveNext();
                scanner.MoveNext();
                scanner.MoveNext();
            }
        }

        [TestMethod]
        public void MoveNext_AfterRemKeyword_ReadsComment()
        {
            using (var scanner = MakeScanner(" REM    example of a comment "))
            {
                scanner.MoveNext();

                Assert.AreEqual(Token.Comment, scanner.CurrentToken);
            }
        }

        [TestMethod]
        public void MoveNext_AfterRemKeyword_SkipsWhitespaces()
        {
            using (var scanner = MakeScanner(" REM    example of a comment "))
            {
                scanner.MoveNext();

                Assert.AreEqual("example of a comment ", scanner.CurrentText);
            }
        }
    }
}
