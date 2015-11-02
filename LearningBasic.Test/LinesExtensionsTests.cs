namespace LearningBasic.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using LearningBasic.Parsing.Ast.Statements;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LinesExtensionsTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToPrintable_WithNullLines_ThrowsArgumentNullException()
        {
            var actual = LinesExtensions.ToPrintable(null);
        }

        [TestMethod]
        public void ToPrintable_WithEmptyLines_ReturnsEmptyArray()
        {
            var emptyLines = Enumerable.Empty<KeyValuePair<int, IStatement>>();

            var actual = emptyLines.ToPrintable();

            Assert.AreEqual(0, actual.Count());
        }

        [TestMethod]
        public void ToPrintable_With10Quit_Returns10Quit()
        {
            var lines = new[] { new KeyValuePair<int, IStatement>(10, new Quit()) };

            var actual = lines.ToPrintable()
                              .Single();

            Assert.AreEqual("10 QUIT", actual);
        }

        [TestMethod]
        public void GetEnoughWideFormat_WithNegativeNumber_ReturnsSingleDigitFormat()
        {
            var actual = LinesExtensions.GetEnoughWideFormat(-100);

            Assert.AreEqual("{0,1}", actual);
        }

        [TestMethod]
        public void GetEnoughWideFormat_WithZeroNumber_ReturnsSingleDigitFormat()
        {
            var actual = LinesExtensions.GetEnoughWideFormat(0);

            Assert.AreEqual("{0,1}", actual);
        }

        [TestMethod]
        public void GetEnoughWideFormat_WithSingleDigitNumber_ReturnsSingleDigitFormat()
        {
            var actual = LinesExtensions.GetEnoughWideFormat(5);

            Assert.AreEqual("{0,1}", actual);
        }

        [TestMethod]
        public void GetEnoughWideFormat_WithThreeDigitNumber_ReturnsThreeDigitFormat()
        {
            var actual = LinesExtensions.GetEnoughWideFormat(500);

            Assert.AreEqual("{0,3}", actual);
        }

        [TestMethod]
        public void GetEnoughWideFormat_WithFiveDigitNumber_ReturnsFiveDigitFormat()
        {
            var actual = LinesExtensions.GetEnoughWideFormat(50000);

            Assert.AreEqual("{0,5}", actual);
        }

        [TestMethod]
        public void GetEnoughWideFormat_WithSixDigitNumber_ReturnsFiveDigitFormat()
        {
            var actual = LinesExtensions.GetEnoughWideFormat(500000);

            Assert.AreEqual("{0,5}", actual);
        }
    }
}
