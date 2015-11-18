namespace LearningInterpreter.Parsing.Code
{
    using LearningInterpreter.Basic.Code.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UnaryOperatorTests
    {
        [TestMethod]
        public void MustEncloseOperandInParentheses_WhenOperandHasLowerPriority_ReturnsTrue()
        {
            var operand = new Add(new Constant("1"), new Constant("2"));
            var expression = new Negative(operand);

            Assert.IsTrue(expression.MustEncloseOperandInParentheses);
        }

        [TestMethod]
        public void MustEncloseOperandInParentheses_WhenOperandHasSamePriority_ReturnsFalse()
        {
            var operand = new Positive(new Constant("1"));
            var expression = new Negative(operand);

            Assert.IsFalse(expression.MustEncloseOperandInParentheses);
        }

        [TestMethod]
        public void MustEncloseOperandInParentheses_WhenOperandHasHigherPriority_ReturnsFalse()
        {
            var operand = new ArrayVariable("A", new[] { new Constant("1") });
            var expression = new Negative(operand);

            Assert.IsFalse(expression.MustEncloseOperandInParentheses);
        }
    }
}
