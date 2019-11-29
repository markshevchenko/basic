namespace LearningInterpreter.Parsing.Code.Statements
{
    using LearningInterpreter.Basic.Code.Expressions;
    using LearningInterpreter.Basic.Code.Statements;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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

        [TestMethod]
        public void ToString_OfForNext_Converts()
        {
            var forNext = new ForNext(new ScalarVariable("I"),
                                      new Constant("1"),
                                      new Constant("5"),
                                      new Constant("1"),
                                      new PrintLine(new[] { new ScalarVariable("I") }));

            var actual = forNext.ToString();

            Assert.AreEqual("FOR I = 1 TO 5 STEP 1 PRINT I NEXT", actual);
        }
    }
}
