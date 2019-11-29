namespace LearningInterpreter.Parsing.Code.Statements
{
    using LearningInterpreter.Basic.Code.Expressions;
    using LearningInterpreter.Basic.Code.Statements;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
