namespace LearningBasic.Parsing.Code.Statements
{
    using LearningBasic.Parsing.Code.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ForTests : BaseTests
    {
        [TestMethod]
        public void Execute_OfFor_RepeatsInnerStatement()
        {
            var rte = MakeRunTimeEnvironment();
            var i = new ScalarVariable("I");
            var from = new Constant(1);
            var to = new Constant(10);
            var shoudBeEqualTo50 = 0;
            rte.AddOrUpdate(new Line("20", new For(i, from, to, new Constant(1))));
            rte.AddOrUpdate(new Line("30", MakeStatement(() => { shoudBeEqualTo50 += 5; })));
            rte.AddOrUpdate(new Line("40", new Next()));
            rte.Run();

            Assert.AreEqual(50, shoudBeEqualTo50);
        }
    }
}
