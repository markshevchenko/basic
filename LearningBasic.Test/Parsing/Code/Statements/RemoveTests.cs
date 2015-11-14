using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace LearningBasic.Parsing.Code.Statements
{
    [TestClass]
    public class RemoveTests : BaseTests
    {
        [TestMethod]
        public void Execute_OfRemove_RemovesSpecifiedLines()
        {
            var rte = MakeRunTimeEnvironment();
            rte.AddOrUpdate(new Line("10", new Nop()));
            rte.AddOrUpdate(new Line("20", new Nop()));
            rte.AddOrUpdate(new Line("30", new Nop()));
            var remove = new Remove(new Range(20));

            remove.Execute(rte);
            var actual = rte.Lines.Select(line => line.Label).ToList();

            CollectionAssert.AreEqual(new[] { "10", "30" }, actual);
        }

        [TestMethod]
        public void ToString_OfRemove_Converts()
        {
            var remove = new Remove(new Range(30, 50));

            var actual = remove.ToString();

            Assert.AreEqual("REMOVE 30-50", actual);
        }
    }
}
