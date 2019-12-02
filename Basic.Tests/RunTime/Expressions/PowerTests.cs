namespace Basic.Tests.Expressions
{
    using Basic.Runtime;
    using Basic.Runtime.Expressions;
    using Microsoft.CSharp.RuntimeBinder;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PowerTests : BaseTests
    {
        [TestMethod]
        public void Power_WithInteger3Integer2_ReturnsInteger9()
        {
            var variables = MakeVariables();
            var left = new Constant(3);
            var right = new Constant(2);
            var expression = new Power(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(9, actual);
        }

        [TestMethod]
        public void Power_WithDouble3Integer2_ReturnsDouble9()
        {
            var variables = MakeVariables();
            var left = new Constant(3.0);
            var right = new Constant(2);
            var expression = new Power(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(9.0, actual);
        }

        [TestMethod]
        public void Power_WithDouble3Double2_ReturnsDouble9()
        {
            var variables = MakeVariables();
            var left = new Constant(3.0);
            var right = new Constant(2.0);
            var expression = new Power(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(9.0, actual);
        }

        [TestMethod]
        public void Power_WithString3Integer2_ReturnsInteger9()
        {
            var variables = MakeVariables();
            var left = new Constant("3");
            var right = new Constant(2);
            var expression = new Power(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(9, actual);
        }

        [TestMethod]
        public void Power_WithString3String2_ReturnsInteger9()
        {
            var variables = MakeVariables();
            var left = new Constant("3");
            var right = new Constant("2");
            var expression = new Power(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(9, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(RuntimeBinderException))]
        public void Power_WithString3StringA_ThrowsRuntimeBinderException()
        {
            var variables = MakeVariables();
            var left = new Constant("3");
            var right = new Constant("A");
            var expression = new Power(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();
        }
    }
}
