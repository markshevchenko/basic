namespace LearningBasic.Test.Parsing
{
    using LearningBasic.Parsing;
    using LearningBasic.Parsing.Ast;
    using LearningBasic.Parsing.Ast.Expressions;
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

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void ReadExpressions_WithEmptyString_ThrowsParserException()
        {
            var scanner = MakeScanner("");

            var actual = scanner.ReadExpressions();
        }

        [TestMethod]
        public void ReadExpressions_WithTwoExpressions_ReturnsListWithTwoElements()
        {
            var scanner = MakeScanner("1, 2");

            var actual = scanner.ReadExpressions();

            Assert.AreEqual(2, actual.Count);
        }

        [TestMethod]
        public void ReadExpression_WithPlusMinues_PlacesMinusToRoot()
        {
            var scanner = MakeScanner("a + b - c");

            var value = scanner.ReadExpression();

            Assert.IsInstanceOfType(value, typeof(Subtract));
        }

        [TestMethod]
        public void ReadExpression_WithPlusMinus_PlacesPlusToLeftChild()
        {
            var scanner = MakeScanner("a + b - c");

            var value = scanner.ReadExpression();
            value = (value as Subtract).Left;

            Assert.IsInstanceOfType(value, typeof(Add));
        }
    }
}
