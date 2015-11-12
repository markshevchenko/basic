namespace LearningBasic.Parsing.Code.Expressions
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ConstantTests
    {
        [TestMethod]
        public void Constant_WithNull_StoresNull()
        {
            var constant = new Constant(null);

            Assert.IsNull(constant.Value);
        }

        [TestMethod]
        public void Constant_WithStringWithQuote_StoresItAsIs()
        {
            var constant = new Constant("string with a \" character");

            Assert.AreEqual("string with a \" character", constant.Value);
        }

        [TestMethod]
        public void Constant_WithDouble_StoresItAsIs()
        {
            var constant = new Constant("3.14159265");
            var actual = constant.Value;

            Assert.AreEqual(3.14159265, actual);
        }

        [TestMethod]
        public void ToString_WithNull_ReturnsEmptyString()
        {
            var constant = new Constant(null);
            var actual = constant.ToString();

            Assert.AreEqual("", actual);
        }

        [TestMethod]
        public void ToString_WithStringWithQuote_DoublesQuote()
        {
            var constant = new Constant("string with a \" character");
            var actual = constant.ToString();

            Assert.AreEqual("\"string with a \"\" character\"", actual);
        }

        [TestMethod]
        public void ToString_WithDouble_ReturnsItWithDecimalPoint()
        {
            var constant = new Constant("3.14159265");
            var actual = constant.ToString();

            Assert.AreEqual("3.14159265", actual);
        }
    }
}
