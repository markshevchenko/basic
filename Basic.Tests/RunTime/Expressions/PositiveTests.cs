namespace Basic.Tests.Expressions
{
    using Basic.Runtime;
    using Basic.Runtime.Expressions;
    using Microsoft.CSharp.RuntimeBinder;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PositiveTests : BaseTests
    {
        [TestMethod]
        public void Positive_WithInteger2_ReturnsInteger2()
        {
            var variables = MakeVariables();
            var operand = new Constant(2);
            var expression = new Positive(operand);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(2, actual);
        }

        [TestMethod]
        public void Positive_WithDouble2_ReturnsDouble2()
        {
            var variables = MakeVariables();
            var operand = new Constant(2.0);
            var expression = new Positive(operand);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(2.0, actual);
        }

        [TestMethod]
        public void Positive_WithString2_ReturnsInteger2()
        {
            var variables = MakeVariables();
            var operand = new Constant("2");
            var expression = new Positive(operand);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(2, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(RuntimeBinderException))]
        public void Positive_WithStringA_ThrowsRuntimeBinderException()
        {
            var variables = MakeVariables();
            var operand = new Constant("A");
            var expression = new Positive(operand);

            var actual = expression.GetExpression(variables)
                                   .Calculate();
        }
    }
}
