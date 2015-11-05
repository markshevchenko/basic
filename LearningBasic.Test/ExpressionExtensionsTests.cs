namespace LearningBasic.Test
{
    using System;
    using System.Globalization;
    using System.Linq.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ExpressionExtensionsTests : BaseTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CalculateValue_WithNull_ThrowsArgumentNullException()
        {
            var value = ExpressionExtensions.CalculateValue(null);
        }

        [TestMethod]
        public void CalculateValue_WithConstantExpression_ReturnsConstant()
        {
            var constant = Expression.Constant(2);

            var actual = constant.CalculateValue();

            Assert.AreEqual(2, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToString_WithNullExpression_ThrowsArgumentNullException()
        {
            var value = ExpressionExtensions.ToString(null, CultureInfo.InvariantCulture);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToString_WithNullFormatProvider_ThrowsArgumentNullException()
        {
            var constant = Expression.Constant(3.14159265);

            var value = constant.ToString(null);
        }

        [TestMethod]
        public void ToString_WithInvariantCulture_ReturnsDecimalPoint()
        {
            var constant = Expression.Constant(3.14159265);

            var value = constant.ToString(CultureInfo.InvariantCulture);

            Assert.AreEqual("3.14159265", value);
        }

        [TestMethod]
        public void ToString_WithRussianCulture_ReturnsDecimalComma()
        {
            var constant = Expression.Constant(3.14159265);

            var value = constant.ToString(CultureInfo.GetCultureInfo("ru"));

            Assert.AreEqual("3,14159265", value);
        }
    }
}
