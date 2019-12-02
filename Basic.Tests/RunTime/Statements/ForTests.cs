namespace Basic.Tests.Statements
{
    using Basic.Runtime;
    using Basic.Runtime.Expressions;
    using Basic.Runtime.Statements;
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

        [TestMethod]
        public void ToString_OfFor_Converts()
        {
            var @for = new For(new ScalarVariable("I"),
                               new Constant("1"),
                               new Constant("5"),
                               new Constant("1"));

            var actual = @for.ToString();

            Assert.AreEqual("FOR I = 1 TO 5 STEP 1", actual);
        }
    }
}
