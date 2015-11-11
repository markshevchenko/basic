using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LearningBasic.Parsing.Code.Expressions
{
    [TestClass]
    public class FunctionTests : BaseTests
    {
        [TestMethod]
        public void Function_WithName_UppersName()
        {
            var function = new Function("name", new IExpression[0]);

            Assert.AreEqual("NAME", function.Name);
        }

        [TestMethod]
        public void GetExpression_WithoutArgs_ReturnsExpectedValue()
        {
            var variables = MakeVariables();
            var args = new IExpression[0];
            var function = new Function(BuiltInFunctions.TestFunctionName, args);
            var expression = function.GetExpression(variables);

            var actual = expression.Calculate();

            Assert.AreEqual(BuiltInFunctions.TestExpected, actual);
        }

        [TestMethod]
        public void GetExpression_With1Arg_ReturnsExpectedValue()
        {
            var variables = MakeVariables();
            var args = new[] { new Constant(1) };
            var function = new Function(BuiltInFunctions.TestFunctionName, args);
            var expression = function.GetExpression(variables);

            var actual = expression.Calculate();

            Assert.AreEqual(BuiltInFunctions.TestExpected, actual);
        }

        [TestMethod]
        public void GetExpression_With2Args_ReturnsExpectedValue()
        {
            var variables = MakeVariables();
            var args = new[] { new Constant(1), new Constant(2) };
            var function = new Function(BuiltInFunctions.TestFunctionName, args);
            var expression = function.GetExpression(variables);

            var actual = expression.Calculate();

            Assert.AreEqual(BuiltInFunctions.TestExpected, actual);
        }

        [TestMethod]
        public void GetExpression_With3Args_ReturnsExpectedValue()
        {
            var variables = MakeVariables();
            var args = new[] { new Constant(1), new Constant(2), new Constant(3) };
            var function = new Function(BuiltInFunctions.TestFunctionName, args);
            var expression = function.GetExpression(variables);

            var actual = expression.Calculate();

            Assert.AreEqual(BuiltInFunctions.TestExpected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetExpression_With4Args_ThrowsInvalidOperationException()
        {
            var variables = MakeVariables();
            var args = new[] { new Constant(1), new Constant(2), new Constant(3), new Constant(4) };
            var function = new Function(BuiltInFunctions.TestFunctionName, args);
            var expression = function.GetExpression(variables);

            var actual = expression.Calculate();
        }

        [TestMethod]
        public void ToString_With1Args_ReturnsFunctionCall()
        {
            var variables = MakeVariables();
            var args = new[] { new Constant(1) };
            var function = new Function(BuiltInFunctions.TestFunctionName, args);

            var actual = function.ToString();

            Assert.AreEqual("_TEST(1)", actual);
        }
    }
}
