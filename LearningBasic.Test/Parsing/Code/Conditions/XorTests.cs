namespace LearningBasic.Parsing.Code.Conditions
{
    using LearningBasic.Parsing.Code.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class XorTests : BaseTests
    {
        [TestMethod]
        public void Xor_WithFalseFalse_ReturnsFalse()
        {
            var left = new Constant(false);
            var right = new Constant(false);

            var operation = new Xor(left, right);
            var actual = (bool)operation.GetExpression(null).Calculate();

            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void Xor_WithTrueFalse_ReturnsTrue()
        {
            var left = new Constant(false);
            var right = new Constant(true);

            var operation = new Xor(left, right);
            var actual = (bool)operation.GetExpression(null).Calculate();

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void Xor_WithTrueTrue_ReturnsFalse()
        {
            var left = new Constant(true);
            var right = new Constant(true);

            var operation = new Xor(left, right);
            var actual = (bool)operation.GetExpression(null).Calculate();

            Assert.AreEqual(false, actual);
        }
    }
}
