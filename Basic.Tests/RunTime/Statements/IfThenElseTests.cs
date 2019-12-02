namespace Basic.Tests.Statements
{
    using Basic.Runtime.Conditions;
    using Basic.Runtime.Expressions;
    using Basic.Runtime.Statements;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class IfThenElseTests : BaseTests
    {
        [TestMethod]
        public void Execute_OfIfWithTrue_ExecutesThen()
        {
            var rte = MakeRunTimeEnvironment();
            var shouldBeEqualTo1 = 0;

            var ifThenElse = new IfThenElse(new Constant(true), MakeStatement(() => { shouldBeEqualTo1 = 1; }));
            ifThenElse.Execute(rte);

            Assert.AreEqual(1, shouldBeEqualTo1);
        }

        [TestMethod]
        public void Execute_OfIfWithFalse_DoesntExecuteThen()
        {
            var rte = MakeRunTimeEnvironment();
            var shouldBeEqualTo0 = 0;

            var ifThenElse = new IfThenElse(new Constant(false), MakeStatement(() => { shouldBeEqualTo0 = 1; }));
            ifThenElse.Execute(rte);

            Assert.AreEqual(0, shouldBeEqualTo0);
        }

        [TestMethod]
        public void Execute_OfIfWithFalse_ExecutesElse()
        {
            var rte = MakeRunTimeEnvironment();
            var shouldBeEqualTo2 = 0;

            var ifThenElse = new IfThenElse(new Constant(false), MakeStatement(() => { shouldBeEqualTo2 = 1; }), MakeStatement(() => { shouldBeEqualTo2 = 2; }));
            ifThenElse.Execute(rte);

            Assert.AreEqual(2, shouldBeEqualTo2);
        }

        [TestMethod]
        public void Execute_OfIfThenElse_Converts()
        {
            var ifThenElse = new IfThenElse(new GreaterThan(new ScalarVariable("A"),
                                                            new Constant("1")),
                                            new PrintLine(new[] { new ScalarVariable("A") }));

            var actual = ifThenElse.ToString();

            Assert.AreEqual("IF A > 1 THEN PRINT A", actual);
        }
    }
}
