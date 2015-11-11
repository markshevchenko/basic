using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LearningBasic.Parsing.Code.Expressions;

namespace LearningBasic.Parsing.Code.Statements
{
    [TestClass]
    public class ForNextTests : BaseTests
    {
        [TestMethod]
        public void Execute_OfForNext_RepeatsInnerStatement()
        {
            var rte = MakeRunTimeEnvironment();
            var i = new ScalarVariable("I");
            var from = new Constant(1);
            var to = new Constant(10);
            var step = new Constant(1);
            var shoudBeEqualTo50 = 0;
            var forNext = new ForNext(i, from, to, step, MakeStatement(() => { shoudBeEqualTo50 += 5; }));

            forNext.Execute(rte);

            Assert.AreEqual(50, shoudBeEqualTo50);
        }
    }
}
