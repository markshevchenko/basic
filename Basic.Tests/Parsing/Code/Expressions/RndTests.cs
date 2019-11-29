namespace LearningInterpreter.Parsing.Code.Expressions
{
    using System;
    using LearningInterpreter.Basic.Code;
    using LearningInterpreter.Basic.Code.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RndTests : BaseTests
    {
        [TestMethod]
        public void Rnd_WithouArguments_ReturnsDouble()
        {
            var variables = MakeVariables();
            variables.Random = new Random();
            var args = new IExpression[0];
            var rnd = new Rnd(args);

            var value = rnd.GetExpression(variables)
                           .Calculate();

            Assert.IsInstanceOfType(value, typeof(double));
        }

        [TestMethod]
        public void Rnd_With1IntegerArg_ReturnsInteger()
        {
            var variables = MakeVariables();
            variables.Random = new Random();
            var args = new[] { new Constant(100) };
            var rnd = new Rnd(args);

            var value = rnd.GetExpression(variables)
                           .Calculate();

            Assert.IsInstanceOfType(value, typeof(int));
        }

        [TestMethod]
        public void Rnd_With2IntegerArgs_ReturnsInteger()
        {
            var variables = MakeVariables();
            variables.Random = new Random();
            var args = new[] { new Constant(100), new Constant(200) };
            var rnd = new Rnd(args);

            var value = rnd.GetExpression(variables)
                           .Calculate();

            Assert.IsInstanceOfType(value, typeof(int));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Rnd_With3Args_ThrowsInvalidOperationException()
        {
            var variables = MakeVariables();
            variables.Random = new Random();
            var args = new[] { new Constant(100), new Constant(200), new Constant(300) };
            var rnd = new Rnd(args);

            var value = rnd.GetExpression(variables)
                           .Calculate();
        }
    }
}
