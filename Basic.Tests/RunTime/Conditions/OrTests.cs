﻿namespace Basic.Tests.Runtime.Conditions
{
    using Basic.Runtime;
    using Basic.Runtime.Conditions;
    using Basic.Runtime.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class OrTests : BaseTests
    {
        [TestMethod]
        public void Or_WithFalseFalse_ReturnsFalse()
        {
            var left = new Constant(false);
            var right = new Constant(false);

            var operation = new Or(left, right);
            var actual = (bool)operation.GetExpression(null).Calculate();

            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void Or_WithTrueFalse_ReturnsTrue()
        {
            var left = new Constant(true);
            var right = new Constant(true);

            var operation = new Or(left, right);
            var actual = (bool)operation.GetExpression(null).Calculate();

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void Or_WithTrueTrue_ReturnsTrue()
        {
            var left = new Constant(true);
            var right = new Constant(true);

            var operation = new Or(left, right);
            var actual = (bool)operation.GetExpression(null).Calculate();

            Assert.AreEqual(true, actual);
        }
    }
}
