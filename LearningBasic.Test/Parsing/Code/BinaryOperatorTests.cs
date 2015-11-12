namespace LearningBasic.Parsing.Code
{
    using LearningBasic.Parsing.Code.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BinaryOperatorTests
    {
        [TestMethod]
        public void MustEncloseLeftOperandInParentheses_WhenLeftOperandHasLowerPriority_ReturnsTrue()
        {
            var leftOperand = new Add(new Constant("1"), new Constant("2"));
            var rightOperand = new Constant("3");
            var binaryOperator = new Multiply(leftOperand, rightOperand);

            Assert.IsTrue(binaryOperator.MustEncloseLeftOperandInParentheses);
        }

        [TestMethod]
        public void MustEncloseLeftOperandInParentheses_WhenOperatorHasLeftAssiciativityAndLeftOperandHasEqualPriority_ReturnsFalse()
        {
            var leftOperand = new Add(new Constant("1"), new Constant("2"));
            var rightOperand = new Constant("3");
            var binaryOperator = new Add(leftOperand, rightOperand);

            Assert.IsFalse(binaryOperator.MustEncloseLeftOperandInParentheses);
        }

        [TestMethod]
        public void MustEncloseLeftOperandInParentheses_WhenOperatorHasRightAssiciativityAndLeftOperandHasEqualPriority_ReturnsTrue()
        {
            var leftOperand = new Power(new Constant("1"), new Constant("2"));
            var rightOperand = new Constant("3");
            var binaryOperator = new Power(leftOperand, rightOperand);

            Assert.IsTrue(binaryOperator.MustEncloseLeftOperandInParentheses);
        }

        [TestMethod]
        public void MustEncloseLeftOperandInParentheses_WhenLeftOperandHasHigherPriority_ReturnsFalse()
        {
            var leftOperand = new Multiply(new Constant("1"), new Constant("2"));
            var rightOperand = new Constant("3");
            var binaryOperator = new Add(leftOperand, rightOperand);

            Assert.IsFalse(binaryOperator.MustEncloseLeftOperandInParentheses);
        }

        [TestMethod]
        public void MustEncloseRightOperandInParentheses_WhenRightOperandHasLowerPriority_ReturnsTrue()
        {
            var leftOperand = new Constant("3");
            var rightOperand = new Add(new Constant("1"), new Constant("2"));
            var binaryOperator = new Multiply(leftOperand, rightOperand);

            Assert.IsTrue(binaryOperator.MustEncloseRightOperandInParentheses);
        }

        [TestMethod]
        public void MustEncloseRightOperandInParentheses_WhenOperatorHasRightAssiciativityAndRightOperandHasEqualPriority_ReturnsFalse()
        {
            var leftOperand = new Constant("3");
            var rightOperand = new Power(new Constant("1"), new Constant("2"));
            var binaryOperator = new Power(leftOperand, rightOperand);

            Assert.IsFalse(binaryOperator.MustEncloseRightOperandInParentheses);
        }

        [TestMethod]
        public void MustEncloseRightOperandInParentheses_WhenOperatorHasLeftAssiciativityAndRightOperandHasEqualPriority_ReturnsTrue()
        {
            var leftOperand = new Constant("3");
            var rightOperand = new Add(new Constant("1"), new Constant("2"));
            var binaryOperator = new Add(leftOperand, rightOperand);

            Assert.IsTrue(binaryOperator.MustEncloseRightOperandInParentheses);
        }

        [TestMethod]
        public void MustEncloseRightOperandInParentheses_WhenRightOperandHasHigherPriority_ReturnsFalse()
        {
            var leftOperand = new Constant("3");
            var rightOperand = new Multiply(new Constant("1"), new Constant("2"));
            var binaryOperator = new Add(leftOperand, rightOperand);

            Assert.IsFalse(binaryOperator.MustEncloseRightOperandInParentheses);
        }
    }
}
