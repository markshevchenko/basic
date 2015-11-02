namespace LearningBasic.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RunTimeEnvironmentTests : BaseTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RunTimeEnvironment_WithNullInputOutput_ThrowsArgumentNullException()
        {
            IInputOutput inputOutput = null;
            IProgramRepository programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RunTimeEnvironment_WithNullProgramRepository_ThrowsArgumentNullException()
        {
            IInputOutput inputOutput = MakeInputOutput();
            IProgramRepository programRepository = null;
            var rte = new RunTimeEnvironment(inputOutput, programRepository);
        }

        [TestMethod]
        public void RunTimeEnvironment_AfterConstructing_IsNotClosed()
        {
            IInputOutput inputOutput = MakeInputOutput("any string");
            IProgramRepository programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);

            Assert.IsFalse(rte.IsClosed);
        }

        [TestMethod]
        public void Close_WhenCalled_SetsIsClosedProperty()
        {
            IInputOutput inputOutput = MakeInputOutput("any string");
            IProgramRepository programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);

            rte.Close();

            Assert.IsTrue(rte.IsClosed);
        }
    }
}
