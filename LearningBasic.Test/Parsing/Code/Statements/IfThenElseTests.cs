namespace LearningBasic.Parsing.Code.Statements
{
    using LearningBasic.Parsing.Code.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class IfThenElseTests : BaseTests
    {
        [TestMethod]
        public void Execute_OfIfWithTrue_ExecutesThen()
        {
            var rte = MakeRunTimeEnvironment();
            var shouldBeEqualsTo1 = 0;

            var ifThenElse = new IfThenElse(new Constant(true), MakeStatement(() => { shouldBeEqualsTo1 = 1; }));
            ifThenElse.Execute(rte);

            Assert.AreEqual(1, shouldBeEqualsTo1);
        }

        [TestMethod]
        public void Execute_OfIfWithFalse_DoesntExecuteThen()
        {
            var rte = MakeRunTimeEnvironment();
            var shouldBeEqualsTo0 = 0;

            var ifThenElse = new IfThenElse(new Constant(false), MakeStatement(() => { shouldBeEqualsTo0 = 1; }));
            ifThenElse.Execute(rte);

            Assert.AreEqual(0, shouldBeEqualsTo0);
        }

        [TestMethod]
        public void Execute_OfIfWithFalse_ExecutesElse()
        {
            var rte = MakeRunTimeEnvironment();
            var shouldBeEqualsTo2 = 0;

            var ifThenElse = new IfThenElse(new Constant(false), MakeStatement(() => { shouldBeEqualsTo2 = 1; }), MakeStatement(() => { shouldBeEqualsTo2 = 2; }));
            ifThenElse.Execute(rte);

            Assert.AreEqual(2, shouldBeEqualsTo2);
        }
    }
}
