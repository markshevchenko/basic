namespace LearningBasic.Parsing.Code.Statements
{
    using LearningBasic.Parsing.Code.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PrintTests : BaseTests
    {
        [TestMethod]
        public void Execute_OfPrint_PrintsValue()
        {
            var inputOutput = MakeInputOutput();
            var rte = MakeRunTimeEnvironment(inputOutput);
            var print = new Print(new[] { new Constant("ABC") });

            print.Execute(rte);

            Assert.AreEqual("ABC", inputOutput.OutputStrings[0]);
        }
    }
}
