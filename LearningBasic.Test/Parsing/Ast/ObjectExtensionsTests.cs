namespace LearningBasic.Test.Parsing.Ast
{
    using LearningBasic.Parsing.Ast;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ObjectExtensionsTests
    {
        [TestMethod]
        public void ToListing_WithNull_ReturnsEmptyString()
        {
            var actual = ObjectExtensions.ToListing(null);

            Assert.AreEqual("", actual);
        }

        [TestMethod]
        public void ToListing_WithStringWithQuote_ReturnsQuotedStringWithDoubleQuote()
        {
            var actual = "string with a \" character".ToListing();

            Assert.AreEqual("\"string with a \"\" character\"", actual);
        }

        [TestMethod]
        public void ToListing_WithDouble_ReturnsDoubleWithDecimalPoint()
        {
            var actual = 3.14159265.ToListing();

            Assert.AreEqual("3.14159265", actual);
        }

        private class Foo
        {
            public override string ToString()
            {
                return "Foo";
            }
        }

        [TestMethod]
        public void ToListing_WithFooObject_ReturnsFoo()
        {
            var actual = new Foo().ToListing();

            Assert.AreEqual("Foo", actual);
        }
    }
}
