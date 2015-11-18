namespace LearningInterpreter.Parsing.Code.Statements
{
    using LearningInterpreter.Basic.Code.Expressions;
    using LearningInterpreter.Basic.Code.Statements;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RandomizeTests : BaseTests
    {
        [TestMethod]
        public void Execute_OfRandomize_CreatesNewRandomObject()
        {
            var rte = MakeRunTimeEnvironment();
            var randomize = new Randomize(new Constant(100));
            var notExpected = rte.Variables.Random;

            randomize.Execute(rte);
            var actual = rte.Variables.Random;

            Assert.AreNotEqual(notExpected, actual);
        }

        [TestMethod]
        public void ToString_OfRandomize_Converts()
        {
            var randomize = new Randomize(new Constant(100));

            var actual = randomize.ToString();

            Assert.AreEqual("RANDOMIZE 100", actual);
        }
    }
}
