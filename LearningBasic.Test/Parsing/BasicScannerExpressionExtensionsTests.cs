namespace LearningBasic.Test.Parsing
{
    using LearningBasic.Parsing;
    using LearningBasic.Parsing.Ast;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BasicScannerExpressionExtensionsTests : BaseTests
    {
        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void ReadLValue_WithConstant_ThrowsParserException()
        {
            var scanner = MakeScanner("123");

            var lValue = scanner.ReadLValue();
        }

        [TestMethod]
        public void TryReadLValue_WithIdentifier_ReturnsTrue()
        {
            var scanner = MakeScanner("foo123");
            ILValue lValue;

            var condition = scanner.TryReadLValue(out lValue);

            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void TryReadLValue_WithArray_ReturnsTrue()
        {
            var scanner = MakeScanner("foo123[x]");
            ILValue lValue;

            var condition = scanner.TryReadLValue(out lValue);

            Assert.IsTrue(condition);
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void TryReadLValue_WithFunction_ThrowsParserException()
        {
            var scanner = MakeScanner("foo123(x)");
            ILValue lValue;

            var condition = scanner.TryReadLValue(out lValue);

            Assert.IsFalse(condition);
        }
    }
}
