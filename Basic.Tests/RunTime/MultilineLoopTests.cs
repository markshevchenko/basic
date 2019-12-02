namespace Basic.Tests.Runtime
{
    using System;
    using Basic.Runtime;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MultilineLoopTests : BaseTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MultilineLoop_WithNullLine_ThrowsArgumentNullException()
        {
            Line line = null;
            ILoop loop = MakeLoop(100);

            var value = new MultilineLoop(line, loop);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MultilineLoop_WithNullLoop_ThrowsArgumentNullException()
        {
            Line line = new Line("10", MakeStatement());
            ILoop loop = null;

            var value = new MultilineLoop(line, loop);
        }

        [TestMethod]
        public void IsOver_AtStart_IsFalse()
        {
            const int countOfIterations = 2;
            Line line = new Line("10", MakeStatement());
            ILoop loop = MakeLoop(countOfIterations);

            var value = new MultilineLoop(line, loop);

            Assert.IsFalse(value.IsOver);
        }

        [TestMethod]
        public void IsOver_AtEnd_IsTrue()
        {
            const int countOfIterations = 2;
            Line line = new Line("10", MakeStatement());
            ILoop loop = MakeLoop(countOfIterations);

            var value = new MultilineLoop(line, loop);

            // Take two iterations of the loop:
            value.TakeStep();
            value.TakeStep();

            Assert.IsTrue(value.IsOver);
        }
    }
}
