namespace LearningBasic.Parsing.Code
{
    using System;
    using LearningBasic.Parsing.Code.Statements;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LineTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Line_WithNullStatement_ThrowsArgumentNullException()
        {
            var value = new Line(null);
        }

        [TestMethod]
        public void Line_WithoutNumber_StoresNullNumber()
        {
            var value = new Line(new Quit());

            Assert.IsNull(value.Number);
        }

        [TestMethod]
        public void Line_WithNumber_StoresThisNumberAsInteger()
        {
            var value = new Line("100", new Quit());

            Assert.AreEqual(100, value.Number);
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void Parse_WithEmptyString_ThrowsParserException()
        {
            var actual = Line.Parse("");
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void Parse_WithNotNumberString_ThrowsParserException()
        {
            var actual = Line.Parse("abcdef");
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void Parse_WithTooLargeNumber_ThrowsParserException()
        {
            var actual = Line.Parse("1234567890987654321");
        }

        [TestMethod]
        public void Parse_WithMinNumber_ReturnsMinNumber()
        {
            var actual = Line.Parse(Line.MinNumber.ToString());

            Assert.AreEqual(Line.MinNumber, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void Parse_WithLessThanMinNumber_ThrowsParserException()
        {
            var outOfRangeNumber = Line.MinNumber - 1;

            var actual = Line.Parse(outOfRangeNumber.ToString());
        }

        [TestMethod]
        public void Parse_WithMaxNumber_ReturnsMaxNumber()
        {
            var actual = Line.Parse(Line.MaxNumber.ToString());

            Assert.AreEqual(Line.MaxNumber, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void Parse_WithGreaterThanMaxNumber_ThrowsParserException()
        {
            var outOfRangeNumber = Line.MaxNumber + 1;

            var actual = Line.Parse(outOfRangeNumber.ToString());
        }

        [TestMethod]
        public void Parse_WithMiddleNumber_ReturnsMiddleNumber()
        {
            var middleNumber = (Line.MaxNumber + Line.MinNumber) / 2;

            var actual = Line.Parse(middleNumber.ToString());

            Assert.AreEqual(middleNumber, actual);
        }
    }
}
