﻿namespace Basic.Tests.Statements
{
    using Basic.Runtime.Expressions;
    using Basic.Runtime.Statements;
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

        [TestMethod]
        public void Execute_OfPrint_Converts()
        {
            var print = new Print(new[] { new Constant("Abc"), new Constant(123) });

            var actual = print.ToString();

            Assert.AreEqual("PRINT \"Abc\", 123;", actual);
        }
    }
}
