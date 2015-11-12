namespace LearningBasic.Parsing.Code.Statements
{
    using LearningBasic.Parsing.Code.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PrintLineTests : BaseTests
    {
        [TestMethod]
        public void Execute_OfPrint_PrintsValue()
        {
            var inputOutput = MakeInputOutput();
            var rte = MakeRunTimeEnvironment(inputOutput);
            var printLine = new PrintLine(new[] { new Constant("ABC") });

            printLine.Execute(rte);

            StringAssert.Contains(inputOutput.LastWritten, "ABC");
        }
    }
}
