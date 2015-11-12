using LearningBasic.Parsing.Code.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LearningBasic.Parsing.Code.Statements
{
    [TestClass]
    public class InputTests : BaseTests
    {
        [TestMethod]
        public void Execute_OfInput_StoresValueFromInput()
        {
            var inputOutput = MakeInputOutput("12345");
            var rte = MakeRunTimeEnvironment(inputOutput);
            var i = new ScalarVariable("I");
            var input = new Input(i);

            input.Execute(rte);
            var actual = rte.Variables["I"];

            Assert.AreEqual(12345, actual);
        }
    }
}
