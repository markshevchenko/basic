namespace LearningBasic.Parsing.Code.Statements
{
    using LearningBasic.Parsing.Code.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PrintLineTests : BaseTests
    {
        [TestMethod]
        public void Execute_OfPrintLine_PrintsValue()
        {
            var inputOutput = MakeInputOutput();
            var rte = MakeRunTimeEnvironment(inputOutput);
            var printLine = new PrintLine(new[] { new Constant("ABC") });

            printLine.Execute(rte);

            Assert.AreEqual("ABC", inputOutput.OutputStrings[0]);
        }

        [TestMethod]
        public void Execute_OfPrint_Converts()
        {
            var printLine = new PrintLine(new[] { new Constant("Abc"), new Constant(123) });

            var actual = printLine.ToString();

            Assert.AreEqual("PRINT \"Abc\", 123", actual);
        }
    }
}
