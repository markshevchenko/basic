namespace LearningBasic.Test.RunTime
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using LearningBasic.RunTime;

    [TestClass]
    public class RunTimeEnvironmentTests : BaseTests
    {
        [TestMethod]
        public void RunTimeEnvironment_AfterConstructing_IsNotClosed()
        {
            var inputOuput = MakeInputOutput("any string");
            var rte = new RunTimeEnvironment(inputOuput);

            Assert.IsFalse(rte.IsClosed);
        }

        [TestMethod]
        public void Close_WhenCalled_SetsIsClosedProperty()
        {
            var inputOuput = MakeInputOutput("any string");
            var rte = new RunTimeEnvironment(inputOuput);

            rte.Close();

            Assert.IsTrue(rte.IsClosed);
        }
    }
}
