﻿namespace LearningInterpreter.Parsing.Code.Expressions
{
    using LearningInterpreter.Basic.Code;
    using LearningInterpreter.Basic.Code.Expressions;
    using Microsoft.CSharp.RuntimeBinder;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class NegativeTests : BaseTests
    {
        [TestMethod]
        public void Negative_WithInteger2_ReturnsIntegerMinus2()
        {
            var variables = MakeVariables();
            var operand = new Constant(2);
            var expression = new Negative(operand);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(-2, actual);
        }

        [TestMethod]
        public void Negative_WithDouble2_ReturnsDoubleMinus2()
        {
            var variables = MakeVariables();
            var operand = new Constant(2.0);
            var expression = new Negative(operand);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(-2.0, actual);
        }

        [TestMethod]
        public void Negative_WithString2_ReturnsIntegerMinus2()
        {
            var variables = MakeVariables();
            var operand = new Constant("2");
            var expression = new Negative(operand);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(-2, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(RuntimeBinderException))]
        public void Negative_WithStringA_ThrowsRuntimeBinderException()
        {
            var variables = MakeVariables();
            var operand = new Constant("A");
            var expression = new Negative(operand);

            var actual = expression.GetExpression(variables)
                                   .Calculate();
        }
    }
}
