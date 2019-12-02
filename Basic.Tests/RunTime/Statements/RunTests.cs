namespace Basic.Tests.Statements
{
    using Basic.Runtime;
    using Basic.Runtime.Statements;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RunTests : BaseTests
    {
        [TestMethod]
        public void Execute_OfRun_RunsProgramLines()
        {
            var shouldBeEqualTo10 = 0;
            var rte = MakeRunTimeEnvironment();
            rte.AddOrUpdate(new Line("10", MakeStatement(() => shouldBeEqualTo10 = 10)));
            var run = new Run();

            run.Execute(rte);

            Assert.AreEqual(10, shouldBeEqualTo10);
        }

        [TestMethod]
        public void Execute_OfRun_Converts()
        {
            var run = new Run();

            var actual = run.ToString();

            Assert.AreEqual("RUN", actual);
        }
    }
}
