namespace LearningInterpreter.Parsing.Code.Conditions
{
    using LearningInterpreter.Basic.Code;
    using LearningInterpreter.Basic.Code.Conditions;
    using LearningInterpreter.Basic.Code.Expressions;
    using LearningInterpreter.Parsing.Code.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AndTests : BaseTests
    {
        [TestMethod]
        public void And_WithFalseFalse_ReturnsFalse()
        {
            var left = new Constant(false);
            var right = new Constant(false);

            var operation = new And(left, right);
            var actual = (bool)operation.GetExpression(null).Calculate();

            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void And_WithTrueFalse_ReturnsFalse()
        {
            var left = new Constant(true);
            var right = new Constant(false);

            var operation = new And(left, right);
            var actual = (bool)operation.GetExpression(null).Calculate();

            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void And_WithTrueTrue_ReturnsTrue()
        {
            var left = new Constant(true);
            var right = new Constant(true);

            var operation = new And(left, right);
            var actual = (bool)operation.GetExpression(null).Calculate();

            Assert.AreEqual(true, actual);
        }
    }
}
