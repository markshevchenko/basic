namespace Basic.Tests.Runtime.Conditions
{
    using Basic.Runtime;
    using Basic.Runtime.Conditions;
    using Basic.Runtime.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class NotTests : BaseTests
    {
        [TestMethod]
        public void Not_WithFalse_ReturnsTrue()
        {
            var operand = new Constant(false);
            var operation = new Not(operand);

            var actual = (bool)operation.GetExpression(null).Calculate();

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void Not_WithTrue_ReturnsFalse()
        {
            var operand = new Constant(true);
            var operation = new Not(operand);

            var actual = (bool)operation.GetExpression(null).Calculate();

            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void ToString_WhenCalled_InsertsSpaceBetweenOperatorAndOperand()
        {
            var operand = new Constant(true);
            var operation = new Not(operand);

            var actual = operation.ToString();

            Assert.AreEqual("NOT True", actual);
        }
    }
}
