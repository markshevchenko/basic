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

        [TestMethod]
        public void ToString_OfInput_Converts()
        {
            var input = new Input("Pro\"mpt", new ScalarVariable("X"));

            var actual = input.ToString();

            Assert.AreEqual("INPUT \"Pro\"\"mpt\", X", actual);
        }
    }
}
