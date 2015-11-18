namespace LearningInterpreter.Parsing.Code.Expressions
{
    using LearningInterpreter.Basic.Code;
    using LearningInterpreter.Basic.Code.Expressions;
    using Microsoft.CSharp.RuntimeBinder;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MultiplyTests : BaseTests
    {
        [TestMethod]
        public void Multiply_WithInteger3Integer2_ReturnsInteger6()
        {
            var variables = MakeVariables();
            var left = new Constant(3);
            var right = new Constant(2);
            var expression = new Multiply(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(6, actual);
        }

        [TestMethod]
        public void Multiply_WithDouble3Integer2_ReturnsDouble6()
        {
            var variables = MakeVariables();
            var left = new Constant(3.0);
            var right = new Constant(2);
            var expression = new Multiply(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(6.0, actual);
        }

        [TestMethod]
        public void Multiply_WithDouble3Double2_ReturnsDouble6()
        {
            var variables = MakeVariables();
            var left = new Constant(3.0);
            var right = new Constant(2.0);
            var expression = new Multiply(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(6.0, actual);
        }

        [TestMethod]
        public void Multiply_WithString3Integer2_ReturnsInteger6()
        {
            var variables = MakeVariables();
            var left = new Constant("3");
            var right = new Constant(2);
            var expression = new Multiply(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(6, actual);
        }

        [TestMethod]
        public void Multiply_WithString3String2_ReturnsInteger6()
        {
            var variables = MakeVariables();
            var left = new Constant("3");
            var right = new Constant("2");
            var expression = new Multiply(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(6, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(RuntimeBinderException))]
        public void Multiply_WithString3StringA_ThrowsRuntimeBinderException()
        {
            var variables = MakeVariables();
            var left = new Constant("3");
            var right = new Constant("A");
            var expression = new Multiply(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();
        }
    }
}
