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

        [TestMethod]
        public void ReadAddOperand_WithMultiplyDivide_PlacesDivideToRoot()
        {
            var scanner = MakeScanner("a * b / c");

            var value = scanner.ReadAddOperand();

            Assert.IsInstanceOfType(value, typeof(Divide));
        }

        [TestMethod]
        public void ReadAddOperand_WithMultiplyDivide_PlacesMultiplyToLeftChild()
        {
            var scanner = MakeScanner("a * b / c");

            var value = scanner.ReadAddOperand();
            value = (value as Divide).Left;

            Assert.IsInstanceOfType(value, typeof(Multiply));
        }

        [TestMethod]
        public void ReadMulOperand_WithUnaryPlusMinus_PlacesPlusToRoot()
        {
            var scanner = MakeScanner("+-a");

            var value = scanner.ReadMulOperand();

            Assert.IsInstanceOfType(value, typeof(Positive));
        }

        [TestMethod]
        public void ReadMulOperand_WithUnaryPlusMinus_PlacesMinusToChild()
        {
            var scanner = MakeScanner("+-a");

            var value = scanner.ReadMulOperand();
            value = (value as Positive).Operand;

            Assert.IsInstanceOfType(value, typeof(Negative));
        }

        [TestMethod]
        public void ReadUnaryOperand_WithCaretCaret_PlacesFirstCaretToRoot()
        {
            var scanner = MakeScanner("a^b^c");

            var value = scanner.ReadUnaryOperand();
            var left = (value as Power).Left;

            Assert.AreEqual("A", (left as ScalarVariable).Name);
        }

        [TestMethod]
        public void ReadUnaryOperand_WithCaretMinus_PlacesMinusToRight()
        {
            var scanner = MakeScanner("a^-b");

            var value = scanner.ReadUnaryOperand();
            value = (value as Power).Right;

            Assert.IsInstanceOfType(value, typeof(Negative));
        }
    }
}
