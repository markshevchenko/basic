namespace Basic.Test.Parsing
{
    using System;
    using Basic.Parsing;
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
        [ExpectedException(typeof(UnexpectedTokenException))]
        public void ReadToken_WithUnexpectedToken_ThrowsUnexpectedTokenException()
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
        public void ReadNode_WithExpectedNode_ReturnsNodeWithExpectedTag()
        {
            using (var scanner = MakeScanner("any text can be here"))
            {
                var node = scanner.ReadNode(s => new AstNode<int>(12), "error");

                Assert.AreEqual(12, node.Tag);
            }
        }

        [TestMethod]
        public void ReadNode_WithExpectedNode_ReturnsNodeWithExpectedText()
        {
            using (var scanner = MakeScanner("any text can be here"))
            {
                var node = scanner.ReadNode(s => new AstNode<int>(12, "node text"), "error");

                Assert.AreEqual("node text", node.Text);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void ReadNode_WithUnexpectedNode_ThrowsParserException()
        {
            using (var scanner = MakeScanner("any text can be here"))
            {
                var node = scanner.ReadNode(s => (AstNode<int>)null, "error");
            }
        }

        [TestMethod]
        public void TryReadNode_WithExcpectedNode_IsTrue()
        {
            using (var scanner = MakeScanner("any text can be here"))
            {
                AstNode<int> node;
                var actual = scanner.TryReadNode(s => new AstNode<int>(12), out node);

                Assert.IsTrue(actual);
            }
        }

        [TestMethod]
        public void TryReadNode_WithExcpectedNode_SetsNode()
        {
            using (var scanner = MakeScanner("any text can be here"))
            {
                var node = new AstNode<int>(1);
                scanner.TryReadNode(s => new AstNode<int>(12), out node);

                Assert.AreEqual(12, node.Tag);
            }
        }

        [TestMethod]
        public void TryReadNode_WithUnexcpectedNode_IsFalse()
        {
            using (var scanner = MakeScanner("any text can be here"))
            {
                AstNode<int> node;
                var actual = scanner.TryReadNode(s => (AstNode<int>)null, out node);

                Assert.IsFalse(actual);
            }
        }

        [TestMethod]
        public void TryReadNode_WithUnexcpectedNode_SetsNodeToNull()
        {
            using (var scanner = MakeScanner("any text can be here"))
            {
                var node = new AstNode<int>(1);
                scanner.TryReadNode(s => (AstNode<int>)null, out node);

                Assert.IsNull(node);
            }
        }
    }
}
