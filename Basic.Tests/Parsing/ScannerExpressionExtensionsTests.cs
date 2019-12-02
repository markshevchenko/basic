namespace Basic.Tests.Parsing
{
    using Basic.Parsing;
    using Basic.Runtime;
    using Basic.Runtime.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ScannerExpressionExtensionsTests : BaseTests
    {
        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void ReadLValue_WithConstant_ThrowsParserException()
        {
            var scanner = new Scanner("123");

            var lValue = scanner.ReadLValue();
        }

        [TestMethod]
        public void TryReadLValue_WithIdentifier_ReturnsTrue()
        {
            var scanner = new Scanner("foo123");
            ILValue lValue;

            var condition = scanner.TryReadLValue(out lValue);

            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void TryReadLValue_WithArray_ReturnsTrue()
        {
            var scanner = new Scanner("foo123[x]");
            ILValue lValue;

            var condition = scanner.TryReadLValue(out lValue);

            Assert.IsTrue(condition);
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void TryReadLValue_WithFunction_ThrowsParserException()
        {
            var scanner = new Scanner("foo123(x)");
            ILValue lValue;

            var condition = scanner.TryReadLValue(out lValue);

            Assert.IsFalse(condition);
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void ReadArray_WithoutLBraket_ThrowsParserException()
        {
            var scanner = new Scanner("foo123 100]");

            var array = scanner.ReadArray();
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void ReadArray_WithoutExpression_ThrowsParserException()
        {
            var scanner = new Scanner("foo123[]");

            var array = scanner.ReadArray();
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void ReadArray_WithoutRBraket_ThrowsParserException()
        {
            var scanner = new Scanner("foo123[100");

            var array = scanner.ReadArray();
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void ReadExpressions_WithEmptyString_ThrowsParserException()
        {
            var scanner = new Scanner("");

            var actual = scanner.ReadExpressions();
        }

        [TestMethod]
        public void ReadExpressions_WithTwoExpressions_ReturnsListWithTwoElements()
        {
            var scanner = new Scanner("1, 2");

            var actual = scanner.ReadExpressions();

            Assert.AreEqual(2, actual.Count);
        }

        [TestMethod]
        public void ReadExpression_WithPlusMinues_PlacesMinusToRoot()
        {
            var scanner = new Scanner("a + b - c");

            var value = scanner.ReadExpression();

            Assert.IsInstanceOfType(value, typeof(Subtract));
        }

        [TestMethod]
        public void ReadExpression_WithPlusMinus_PlacesPlusToLeftChild()
        {
            var scanner = new Scanner("a + b - c");

            var value = scanner.ReadExpression();
            value = (value as Subtract).Left;

            Assert.IsInstanceOfType(value, typeof(Add));
        }

        [TestMethod]
        public void ReadAddOperand_WithMultiplyDivide_PlacesDivideToRoot()
        {
            var scanner = new Scanner("a * b / c");

            var value = scanner.ReadAddOperand();

            Assert.IsInstanceOfType(value, typeof(Divide));
        }

        [TestMethod]
        public void ReadAddOperand_WithMultiplyDivide_PlacesMultiplyToLeftChild()
        {
            var scanner = new Scanner("a * b / c");

            var value = scanner.ReadAddOperand();
            value = (value as Divide).Left;

            Assert.IsInstanceOfType(value, typeof(Multiply));
        }

        [TestMethod]
        public void ReadMulOperand_WithUnaryPlusMinus_PlacesPlusToRoot()
        {
            var scanner = new Scanner("+-a");

            var value = scanner.ReadMulOperand();

            Assert.IsInstanceOfType(value, typeof(Positive));
        }

        [TestMethod]
        public void ReadMulOperand_WithUnaryPlusMinus_PlacesMinusToChild()
        {
            var scanner = new Scanner("+-a");

            var value = scanner.ReadMulOperand();
            value = (value as Positive).Operand;

            Assert.IsInstanceOfType(value, typeof(Negative));
        }

        [TestMethod]
        public void ReadUnaryOperand_WithCaretCaret_PlacesFirstCaretToRoot()
        {
            var scanner = new Scanner("a^b^c");

            var value = scanner.ReadUnaryOperand();
            var left = (value as Power).Left;

            Assert.AreEqual("A", (left as ScalarVariable).Name);
        }

        [TestMethod]
        public void ReadUnaryOperand_WithCaretMinus_PlacesMinusToRight()
        {
            var scanner = new Scanner("a^-b");

            var value = scanner.ReadUnaryOperand();
            value = (value as Power).Right;

            Assert.IsInstanceOfType(value, typeof(Negative));
        }

        [TestMethod]
        public void ReadValue_WithInteger_ReturnsConstant()
        {
            var scanner = new Scanner("123");

            var value = scanner.ReadValue();

            Assert.IsInstanceOfType(value, typeof(Constant));
        }

        [TestMethod]
        public void ReadValue_WithReal_ReturnsConstant()
        {
            var scanner = new Scanner("123.456");

            var value = scanner.ReadValue();

            Assert.IsInstanceOfType(value, typeof(Constant));
        }

        [TestMethod]
        public void ReadValue_WithString_ReturnsConstant()
        {
            var scanner = new Scanner("\"123\"");

            var value = scanner.ReadValue();

            Assert.IsInstanceOfType(value, typeof(Constant));
        }

        [TestMethod]
        public void ReadValue_WithAddInParentheses_ReturnsAdd()
        {
            var scanner = new Scanner("(a + 2)");

            var value = scanner.ReadValue();

            Assert.IsInstanceOfType(value, typeof(Add));
        }

        [TestMethod]
        public void ReadValue_WithIdentifier_ReturnsScalarVariable()
        {
            var scanner = new Scanner("foo123");

            var value = scanner.ReadValue();

            Assert.IsInstanceOfType(value, typeof(ScalarVariable));
        }

        [TestMethod]
        public void ReadValue_WithArray_ReturnsArrayVariable()
        {
            var scanner = new Scanner("foo123[x]");

            var value = scanner.ReadValue();

            Assert.IsInstanceOfType(value, typeof(ArrayVariable));
        }

        [TestMethod]
        public void ReadValue_WithFunction_ReturnsFunction()
        {
            var scanner = new Scanner("foo123(x)");

            var value = scanner.ReadValue();

            Assert.IsInstanceOfType(value, typeof(Function));
        }

        [TestMethod]
        public void ReadValue_WithRnd_ReturnsRnd()
        {
            var scanner = new Scanner("rnd(100)");

            var value = scanner.ReadValue();

            Assert.IsInstanceOfType(value, typeof(Rnd));
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void ReadValue_WithMinus_ThrowsParserException()
        {
            var scanner = new Scanner("-a");

            var value = scanner.ReadValue();
        }
    }
}
