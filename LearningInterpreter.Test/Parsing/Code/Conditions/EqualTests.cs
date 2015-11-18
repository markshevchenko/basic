namespace LearningInterpreter.Parsing.Code.Conditions
{
    using LearningInterpreter.Basic.Code;
    using LearningInterpreter.Basic.Code.Conditions;
    using LearningInterpreter.Basic.Code.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EqualTests
    {
        [TestMethod]
        public void Equal_WithInteger3Integer3_ReturnsTrue()
        {
            var left = new Constant(3);
            var right = new Constant(3);

            var operation = new Equal(left, right);
            var actual = (bool)operation.GetExpression(null).Calculate();

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void Equal_WithInteger3Double3_ReturnsTrue()
        {
            var left = new Constant(3);
            var right = new Constant(3.0);

            var operation = new Equal(left, right);
            var actual = (bool)operation.GetExpression(null).Calculate();

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void Equal_WithDouble3Double3_ReturnsTrue()
        {
            var left = new Constant(3.0);
            var right = new Constant(3.0);

            var operation = new Equal(left, right);
            var actual = (bool)operation.GetExpression(null).Calculate();

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void Equal_WithString3String3_ReturnsTrue()
        {
            var left = new Constant("3");
            var right = new Constant("3");

            var operation = new Equal(left, right);
            var actual = (bool)operation.GetExpression(null).Calculate();

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void Equal_WithString3Integer3_ReturnsTrue()
        {
            var left = new Constant("3");
            var right = new Constant(3);

            var operation = new Equal(left, right);
            var actual = (bool)operation.GetExpression(null).Calculate();

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void Equal_WithString3Double3_ReturnsTrue()
        {
            var left = new Constant("3");
            var right = new Constant(3.0);

            var operation = new Equal(left, right);
            var actual = (bool)operation.GetExpression(null).Calculate();

            Assert.AreEqual(true, actual);
        }
    }
}
