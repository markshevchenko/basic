namespace Basic.Tests.Runtime.Conditions
{
    using Basic.Runtime;
    using Basic.Runtime.Conditions;
    using Basic.Runtime.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LessThanOrEqualTests
    {
        [TestMethod]
        public void LessThanOrEqual_WithInteger3Double2_ReturnFalse()
        {
            var left = new Constant(3);
            var right = new Constant(2.0);

            var operation = new LessThanOrEqual(left, right);
            var actual = (bool)operation.GetExpression(null).Calculate();

            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void LessThanOrEqual_WithDouble2Integer3_ReturnTrue()
        {
            var left = new Constant(2.0);
            var right = new Constant(3);

            var operation = new LessThanOrEqual(left, right);
            var actual = (bool)operation.GetExpression(null).Calculate();

            Assert.AreEqual(true, actual);
        }
    }
}
