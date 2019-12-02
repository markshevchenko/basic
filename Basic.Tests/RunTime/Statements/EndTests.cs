﻿namespace Basic.Tests.Statements
{
    using Basic.Runtime;
    using Basic.Runtime.Statements;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EndTests : BaseTests
    {
        [TestMethod]
        public void Execute_OfEnd_EndsProgram()
        {
            var rte = MakeRunTimeEnvironment();
            var shouldNotBeTrue = false;
            rte.AddOrUpdate(new Line("10", new Nop()));
            rte.AddOrUpdate(new Line("20", new End()));
            rte.AddOrUpdate(new Line("30", MakeStatement(() => { shouldNotBeTrue = true; })));

            rte.Run();

            Assert.IsFalse(shouldNotBeTrue);
        }

        [TestMethod]
        public void ToString_OfEnd_Converts()
        {
            var end = new End();

            var actual = end.ToString();

            Assert.AreEqual("END", actual);
        }
    }
}
