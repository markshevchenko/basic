namespace Basic.Tests.Runtime
{
    using Basic.Runtime;
    using Basic.Runtime.Statements;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RangeExtensionsTests : BaseTests
    {
        [TestMethod]
        public void GetBounds_WithLessRange_ReturnsCount0()
        {
            var rte = MakeRunTimeEnvironment();
            rte.AddOrUpdate(new Line("40", new Nop()));
            rte.AddOrUpdate(new Line("50", new Nop()));
            rte.AddOrUpdate(new Line("60", new Nop()));
            var range = new Range(10, 30);

            int start, count;
            range.GetBounds(rte, out start, out count);

            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void GetBounds_WithGreaterRange_ReturnsCount0()
        {
            var rte = MakeRunTimeEnvironment();
            rte.AddOrUpdate(new Line("40", new Nop()));
            rte.AddOrUpdate(new Line("50", new Nop()));
            rte.AddOrUpdate(new Line("60", new Nop()));
            var range = new Range(70, 90);

            int start, count;
            range.GetBounds(rte, out start, out count);

            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void GetBound_WithSameStart_ReturnsStart0()
        {
            var rte = MakeRunTimeEnvironment();
            rte.AddOrUpdate(new Line("40", new Nop()));
            rte.AddOrUpdate(new Line("50", new Nop()));
            rte.AddOrUpdate(new Line("60", new Nop()));
            var range = new Range(40, 55);

            int start, count;
            range.GetBounds(rte, out start, out count);

            Assert.AreEqual(0, start);
        }

        [TestMethod]
        public void GetBound_WithLastTwoLines_ReturnsCount2()
        {
            var rte = MakeRunTimeEnvironment();
            rte.AddOrUpdate(new Line("40", new Nop()));
            rte.AddOrUpdate(new Line("50", new Nop()));
            rte.AddOrUpdate(new Line("60", new Nop()));
            var range = new Range(50, 60);

            int start, count;
            range.GetBounds(rte, out start, out count);

            Assert.AreEqual(2, count);
        }

        [TestMethod]
        public void GetBound_WithExactlyMiddleTwoLines_ReturnsCount2()
        {
            var rte = MakeRunTimeEnvironment();
            rte.AddOrUpdate(new Line("40", new Nop()));
            rte.AddOrUpdate(new Line("50", new Nop()));
            rte.AddOrUpdate(new Line("60", new Nop()));
            rte.AddOrUpdate(new Line("70", new Nop()));
            var range = new Range(50, 60);

            int start, count;
            range.GetBounds(rte, out start, out count);

            Assert.AreEqual(2, count);
        }

        [TestMethod]
        public void GetBound_WhenEdgingAroundMiddleTwoLines_ReturnsCount2()
        {
            var rte = MakeRunTimeEnvironment();
            rte.AddOrUpdate(new Line("40", new Nop()));
            rte.AddOrUpdate(new Line("50", new Nop()));
            rte.AddOrUpdate(new Line("60", new Nop()));
            rte.AddOrUpdate(new Line("70", new Nop()));
            var range = new Range(45, 65);

            int start, count;
            range.GetBounds(rte, out start, out count);

            Assert.AreEqual(2, count);
        }

        [TestMethod]
        public void GetBound_WhenEdgingAroundMiddleTwoLines_ReturnsStart1()
        {
            var rte = MakeRunTimeEnvironment();
            rte.AddOrUpdate(new Line("40", new Nop()));
            rte.AddOrUpdate(new Line("50", new Nop()));
            rte.AddOrUpdate(new Line("60", new Nop()));
            rte.AddOrUpdate(new Line("70", new Nop()));
            var range = new Range(45, 65);

            int start, count;
            range.GetBounds(rte, out start, out count);

            Assert.AreEqual(2, count);
        }
    }
}
