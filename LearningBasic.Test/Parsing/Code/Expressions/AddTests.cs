namespace LearningBasic.Parsing.Code.Expressions
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AddTests : BaseTests
    {
        [TestMethod]
        public void Add_WithInteger3Integer2_ReturnsInteger5()
        {
            var variables = MakeVariables();
            var left = new Constant(3);
            var right = new Constant(2);
            var expression = new Add(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(5, actual);
        }

        [TestMethod]
        public void Add_WithDouble3Integer2_ReturnsDouble5()
        {
            var variables = MakeVariables();
            var left = new Constant(3.0);
            var right = new Constant(2);
            var expression = new Add(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(5.0, actual);
        }

        [TestMethod]
        public void Add_WithDouble3Double2_ReturnsDouble5()
        {
            var variables = MakeVariables();
            var left = new Constant(3.0);
            var right = new Constant(2.0);
            var expression = new Add(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(5.0, actual);
        }

        [TestMethod]
        public void Add_WithString3Integer2_ReturnsInteger5()
        {
            var variables = MakeVariables();
            var left = new Constant("3");
            var right = new Constant(2);
            var expression = new Add(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(5, actual);
        }

        [TestMethod]
        public void Add_WithString3String2_ReturnsInteger5()
        {
            var variables = MakeVariables();
            var left = new Constant("3");
            var right = new Constant("2");
            var expression = new Add(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(5, actual);
        }

        [TestMethod]
        public void Add_WithString3StringA_ReturnsString3A()
        {
            var variables = MakeVariables();
            var left = new Constant("3");
            var right = new Constant("A");
            var expression = new Add(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual("3A", actual);
        }
    }
}
