namespace LearningBasic.Parsing.Code.Expressions
{
    using Microsoft.CSharp.RuntimeBinder;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SubtractTests : BaseTests
    {
        [TestMethod]
        public void Subtract_WithInteger3Integer2_ReturnsInteger1()
        {
            var variables = MakeVariables();
            var left = new Constant(3);
            var right = new Constant(2);
            var expression = new Subtract(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void Subtract_WithDouble3Integer2_ReturnsDouble1()
        {
            var variables = MakeVariables();
            var left = new Constant(3.0);
            var right = new Constant(2);
            var expression = new Subtract(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(1.0, actual);
        }

        [TestMethod]
        public void Subtract_WithDouble3Double2_ReturnsDouble1()
        {
            var variables = MakeVariables();
            var left = new Constant(3.0);
            var right = new Constant(2.0);
            var expression = new Subtract(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(1.0, actual);
        }

        [TestMethod]
        public void Subtract_WithString3Integer2_ReturnsInteger1()
        {
            var variables = MakeVariables();
            var left = new Constant("3");
            var right = new Constant(2);
            var expression = new Subtract(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void Subtract_WithString3String2_ReturnsInteger1()
        {
            var variables = MakeVariables();
            var left = new Constant("3");
            var right = new Constant("2");
            var expression = new Subtract(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(RuntimeBinderException))]
        public void Subtract_WithString3StringA_ThrowsRuntimeBinderException()
        {
            var variables = MakeVariables();
            var left = new Constant("3");
            var right = new Constant("A");
            var expression = new Subtract(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();
        }
    }
}
