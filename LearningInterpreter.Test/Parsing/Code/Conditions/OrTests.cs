namespace LearningInterpreter.Parsing.Code.Conditions
{
    using LearningInterpreter.Basic.Code;
    using LearningInterpreter.Basic.Code.Conditions;
    using LearningInterpreter.Basic.Code.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class OrTests : BaseTests
    {
        [TestMethod]
        public void Or_WithFalseFalse_ReturnsFalse()
        {
            var left = new Constant(false);
            var right = new Constant(false);

            var operation = new Or(left, right);
            var actual = (bool)operation.GetExpression(null).Calculate();

            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void Or_WithTrueFalse_ReturnsTrue()
        {
            var left = new Constant(true);
            var right = new Constant(true);

            var operation = new Or(left, right);
            var actual = (bool)operation.GetExpression(null).Calculate();

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void Or_WithTrueTrue_ReturnsTrue()
        {
            var left = new Constant(true);
            var right = new Constant(true);

            var operation = new Or(left, right);
            var actual = (bool)operation.GetExpression(null).Calculate();

            Assert.AreEqual(true, actual);
        }
    }
}
