﻿namespace Basic.Tests.Expressions
{
    using Basic.Runtime;
    using Basic.Runtime.Expressions;
    using Microsoft.CSharp.RuntimeBinder;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DivideTests : BaseTests
    {
        [TestMethod]
        public void Divide_WithInteger4Integer2_ReturnsInteger2()
        {
            var variables = MakeVariables();
            var left = new Constant(4);
            var right = new Constant(2);
            var expression = new Divide(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(2, actual);
        }

        [TestMethod]
        public void Divide_WithInteger3Integer2_ReturnsDouble1dot5()
        {
            var variables = MakeVariables();
            var left = new Constant(3);
            var right = new Constant(2);
            var expression = new Divide(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(1.5, actual);
        }

        [TestMethod]
        public void Divide_WithDouble3Integer2_ReturnsDouble1dot5()
        {
            var variables = MakeVariables();
            var left = new Constant(3.0);
            var right = new Constant(2);
            var expression = new Divide(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(1.5, actual);
        }

        [TestMethod]
        public void Divide_WithDouble3Double2_ReturnsDouble1dot5()
        {
            var variables = MakeVariables();
            var left = new Constant(3.0);
            var right = new Constant(2.0);
            var expression = new Divide(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(1.5, actual);
        }

        [TestMethod]
        public void Divide_WithString3Integer2_ReturnsInteger1dot5()
        {
            var variables = MakeVariables();
            var left = new Constant("3");
            var right = new Constant(2);
            var expression = new Divide(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(1.5, actual);
        }

        [TestMethod]
        public void Divide_WithString3String2_ReturnsInteger1dot5()
        {
            var variables = MakeVariables();
            var left = new Constant("3");
            var right = new Constant("2");
            var expression = new Divide(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(1.5, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(RuntimeBinderException))]
        public void Divide_WithString3StringA_ThrowsRuntimeBinderException()
        {
            var variables = MakeVariables();
            var left = new Constant("3");
            var right = new Constant("A");
            var expression = new Divide(left, right);

            var actual = expression.GetExpression(variables)
                                   .Calculate();
        }
    }
}
