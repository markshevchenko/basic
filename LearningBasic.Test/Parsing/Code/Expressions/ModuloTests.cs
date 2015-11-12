namespace LearningBasic.Parsing.Code.Expressions
{
    using Microsoft.CSharp.RuntimeBinder;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ModuloTests : BaseTests
    {
        [TestMethod]
        public void Modulo_WithInteger4Integer2_ReturnsInteger0()
        {
            var variables = MakeVariables();
            var left = new Constant(4);
            var right = new Constant(2);
            var expression = new Modulo(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void Modulo_WithInteger3Integer2_ReturnsInteger1()
        {
            var variables = MakeVariables();
            var left = new Constant(3);
            var right = new Constant(2);
            var expression = new Modulo(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void Modulo_WithDouble3Integer2_ReturnsDouble1()
        {
            var variables = MakeVariables();
            var left = new Constant(3.0);
            var right = new Constant(2);
            var expression = new Modulo(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(1.0, actual);
        }

        [TestMethod]
        public void Modulo_WithDouble3Double2_ReturnsDouble1()
        {
            var variables = MakeVariables();
            var left = new Constant(3.0);
            var right = new Constant(2.0);
            var expression = new Modulo(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(1.0, actual);
        }

        [TestMethod]
        public void Modulo_WithString3Integer2_ReturnsInteger1()
        {
            var variables = MakeVariables();
            var left = new Constant("3");
            var right = new Constant(2);
            var expression = new Modulo(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void Modulo_WithString3String2_ReturnsInteger1()
        {
            var variables = MakeVariables();
            var left = new Constant("3");
            var right = new Constant("2");
            var expression = new Modulo(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(RuntimeBinderException))]
        public void Modulo_WithString3StringA_ThrowsRuntimeBinderException()
        {
            var variables = MakeVariables();
            var left = new Constant("3");
            var right = new Constant("A");
            var expression = new Modulo(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();
        }
    }
}
