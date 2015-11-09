namespace LearningBasic.Parsing.Code
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BuiltInOperatorsTests : BaseTests
    {
        [TestMethod]
        public void Power_WithIntegerPositiveBaseAndExponent_ReturnsInteger()
        {
            var value = BuiltInOperators.Power(2, 3);

            Assert.IsInstanceOfType(value, typeof(int));
        }

        [TestMethod]
        public void Power_WithIntegerPositiveBaseNegativeExponent_ReturnsDouble()
        {
            var value = BuiltInOperators.Power(2, -3);

            Assert.IsInstanceOfType(value, typeof(double));
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void Power_WithIntegerZeroBaseNegativeExponent_ThrowsDivideByZeroException()
        {
            var value = BuiltInOperators.Power(0, -1);
        }

        [TestMethod]
        public void Power_WithInteger2And3_ReturnsInteger8()
        {
            var actual = BuiltInOperators.Power(2, 3);

            Assert.AreEqual(8, actual);
        }

        [TestMethod]
        public void Power_WithInteger2AndNegate3_ReturnsDouble0Dot125()
        {
            var actual = BuiltInOperators.Power(2, -3);

            Assert.AreEqual(0.125, actual);
        }

        [TestMethod]
        public void Power_WithInteger3And2_ReturnsInteger9()
        {
            var actual = BuiltInOperators.Power(3, 2);

            Assert.AreEqual(9, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void Power_WithDoubleZeroBaseIntegerNegativeExponent_ThrowsDivideByZeroException()
        {
            var value = BuiltInOperators.Power(0.0, -1);
        }


        [TestMethod]
        public void Power_WithDouble2AndInteger3_ReturnsDouble8()
        {
            var actual = BuiltInOperators.Power(2.0, 3);

            Assert.AreEqual(8.0, actual);
        }

        [TestMethod]
        public void Power_WithDouble3AndInteger2_ReturnsDouble9()
        {
            var expected = BuiltInOperators.Power(3.0, 2);

            Assert.AreEqual(9.0, expected);
        }

        [TestMethod]
        public void Divide_WithIntegerParametersWithoutRemainder_ReturnsInteger()
        {
            var value = BuiltInOperators.Divide(4, 2);

            Assert.IsInstanceOfType(value, typeof(int));
        }

        [TestMethod]
        public void Divide_WithIntegerParametersWithRemainder_ReturnsDouble()
        {
            var value = BuiltInOperators.Divide(4, 3);

            Assert.IsInstanceOfType(value, typeof(double));
        }
    }
}
