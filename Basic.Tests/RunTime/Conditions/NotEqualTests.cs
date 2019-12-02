namespace Basic.Tests.Runtime.Conditions
{
    using Basic.Runtime;
    using Basic.Runtime.Conditions;
    using Basic.Runtime.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class NotEqualTests
    {
        [TestMethod]
        public void NotEqual_WithInteger3Integer3_ReturnsFalse()
        {
            var left = new Constant(3);
            var right = new Constant(3);

            var operation = new NotEqual(left, right);
            var actual = (bool)operation.GetExpression(null).Calculate();

            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void NotEqual_WithInteger3Double3_ReturnsFalse()
        {
            var left = new Constant(3);
            var right = new Constant(3.0);

            var operation = new NotEqual(left, right);
            var actual = (bool)operation.GetExpression(null).Calculate();

            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void NotEqual_WithDouble3Double3_ReturnsFalse()
        {
            var left = new Constant(3.0);
            var right = new Constant(3.0);

            var operation = new NotEqual(left, right);
            var actual = (bool)operation.GetExpression(null).Calculate();

            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void NotEqual_WithString3String3_ReturnsFalse()
        {
            var left = new Constant("3");
            var right = new Constant("3");

            var operation = new NotEqual(left, right);
            var actual = (bool)operation.GetExpression(null).Calculate();

            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void NotEqual_WithString3Integer3_ReturnsFalse()
        {
            var left = new Constant("3");
            var right = new Constant(3);

            var operation = new NotEqual(left, right);
            var actual = (bool)operation.GetExpression(null).Calculate();

            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void NotEqual_WithString3Double3_ReturnsFalse()
        {
            var left = new Constant("3");
            var right = new Constant(3.0);

            var operation = new NotEqual(left, right);
            var actual = (bool)operation.GetExpression(null).Calculate();

            Assert.AreEqual(false, actual);
        }
    }
}
