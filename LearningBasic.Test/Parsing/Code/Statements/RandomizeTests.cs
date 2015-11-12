using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LearningBasic.Parsing.Code.Expressions;
using LearningBasic.RunTime;

namespace LearningBasic.Parsing.Code.Statements
{
    [TestClass]
    public class RandomizeTests : BaseTests
    {
        [TestMethod]
        public void Execute_OfRandomize_CreatesNewRandomObject()
        {
            var rte = MakeRunTimeEnvironment();
            var randomize = new Randomize(new Constant(100));
            var notExpected = rte.Variables[RunTimeEnvironment.RandomKey];

            randomize.Execute(rte);
            var actual = rte.Variables[RunTimeEnvironment.RandomKey];

            Assert.AreNotEqual(notExpected, actual);
        }
    }
}
