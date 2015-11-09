namespace LearningBasic.RunTime
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using LearningBasic.Parsing;

    [TestClass]
    public class MultilineLoopTests : BaseTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MultilineLoop_WithNullLine_ThrowsArgumentNullException()
        {
            ILine line = null;
            ILoop loop = MakeLoop(100);

            var value = new MultilineLoop(line, loop);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MultilineLoop_WithNullLoop_ThrowsArgumentNullException()
        {
            ILine line = new Line("10", MakeStatement());
            ILoop loop = null;

            var value = new MultilineLoop(line, loop);
        }

        [TestMethod]
        public void IsOver_AtStart_IsFalse()
        {
            const int countOfIterations = 2;
            ILine line = new Line("10", MakeStatement());
            ILoop loop = MakeLoop(countOfIterations);

            var value = new MultilineLoop(line, loop);

            Assert.IsFalse(value.IsOver);
        }

        [TestMethod]
        public void IsOver_AtEnd_IsTrue()
        {
            const int countOfIterations = 2;
            ILine line = new Line("10", MakeStatement());
            ILoop loop = MakeLoop(countOfIterations);

            var value = new MultilineLoop(line, loop);

            // Take two iterations of the loop:
            value.TakeStep();
            value.TakeStep();

            Assert.IsTrue(value.IsOver);
        }
    }
}
