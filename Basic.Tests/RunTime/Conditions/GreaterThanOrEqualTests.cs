namespace Basic.Tests.Runtime.Conditions
{
    using Basic.Runtime;
    using Basic.Runtime.Conditions;
    using Basic.Runtime.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GreaterThanOrEqualTests
    {
        [TestMethod]
        public void GreaterThanOrEqual_WithInteger3Double2_ReturnTrue()
        {
            var left = new Constant(3);
            var right = new Constant(2.0);

            var operation = new GreaterThanOrEqual(left, right);
            var actual = (bool)operation.GetExpression(null).Calculate();

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void GreaterThanOrEqual_WithDouble2Integer3_ReturnFalse()
        {
            var left = new Constant(2.0);
            var right = new Constant(3);

            var operation = new GreaterThanOrEqual(left, right);
            var actual = (bool)operation.GetExpression(null).Calculate();

            Assert.AreEqual(false, actual);
        }
    }
}
