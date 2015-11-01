namespace LearningBasic.Test.Parsing.Ast
{
    using System;
    using LearningBasic.Parsing.Ast;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RangeTests
    {
        [TestMethod]
        public void IsDefined_OfUndefined_IsFalse()
        {
            Assert.IsFalse(Range.Undefined.IsDefined);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Min_OfUndefined_ThrowsInvalidOperationException()
        {
            var min = Range.Undefined.Min;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Max_OfUndefined_ThrowsInvalidOperationException()
        {
            var max = Range.Undefined.Max;
        }

        [TestMethod]
        public void Range_With100_SetsMinTo100()
        {
            var value = new Range(100);

            Assert.AreEqual(100, value.Min);
        }

        [TestMethod]
        public void Range_With100_SetsMaxTo100()
        {
            var value = new Range(100);

            Assert.AreEqual(100, value.Max);
        }

        [TestMethod]
        public void Range_With100_SetsIsDefinedToTrue()
        {
            var value = new Range(100);

            Assert.IsTrue(value.IsDefined);
        }

        [TestMethod]
        public void Range_With100And200_SetsMinTo100()
        {
            var value = new Range(100, 200);

            Assert.AreEqual(100, value.Min);
        }

        [TestMethod]
        public void Range_With100And200_SetsMaxTo200()
        {
            var value = new Range(100, 200);

            Assert.AreEqual(200, value.Max);
        }

        [TestMethod]
        public void Range_With100And200_SetsIsDefinedToTrue()
        {
            var value = new Range(100, 200);

            Assert.IsTrue(value.IsDefined);
        }

        [TestMethod]
        public void Range_With100And100_SetsMinAndMaxTo100()
        {
            var value = new Range(100, 100);

            Assert.IsTrue(value.Min == 100 && value.Max == 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Range_With101And100_ThrowsArgumentException()
        {
            var value = new Range(101, 100);
        }

        [TestMethod]
        public void Contains_OfUndefined_ReturnsTrue()
        {
            var condition = Range.Undefined.Contains(100);

            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void Contains_WithMin_ReturnsTrue()
        {
            var range = new Range(100, 200);

            var condition = range.Contains(100);

            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void Contains_WithLessThanMin_ReturnsFalse()
        {
            var range = new Range(100, 200);

            var condition = range.Contains(99);

            Assert.IsFalse(condition);
        }

        [TestMethod]
        public void Contains_WithMax_ReturnsTrue()
        {
            var range = new Range(100, 200);

            var condition = range.Contains(200);

            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void Contains_WithGreaterThanMax_ReturnsFalse()
        {
            var range = new Range(100, 200);

            var condition = range.Contains(201);

            Assert.IsFalse(condition);
        }

        [TestMethod]
        public void Contains_WithMiddle_ReturnsTrue()
        {
            var range = new Range(100, 200);

            var condition = range.Contains(150);

            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void Contains_WithSameAsSingle_ReturnsTrue()
        {
            var range = new Range(100);

            var condition = range.Contains(100);

            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void ToString_Of100_Returns100()
        {
            var range = new Range(100);

            var actual = range.ToString();

            Assert.AreEqual("100", actual);
        }

        [TestMethod]
        public void ToString_Of100And200_Returns100Minus200()
        {
            var range = new Range(100, 200);

            var actual = range.ToString();

            Assert.AreEqual("100-200", actual);
        }
    }
}
