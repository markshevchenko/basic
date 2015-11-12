namespace LearningBasic.Parsing.Code.Statements
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class QuitTests : BaseTests
    {
        [TestMethod]
        public void Execute_OfQuit_ClosesRunTimeEnvironment()
        {
            var rte = MakeRunTimeEnvironment();
            var quit = new Quit();

            quit.Execute(rte);

            Assert.IsTrue(rte.IsClosed);
        }
    }
}
