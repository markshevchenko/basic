namespace LearningInterpreter.Parsing.Code.Statements
{
    using LearningInterpreter.Basic.Code.Expressions;
    using LearningInterpreter.Basic.Code.Statements;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DimTests : BaseTests
    {
        [TestMethod]
        public void Execute_OfDim_CreatesArray()
        {
            var indexes = new[] { new Constant(100), new Constant(200) };
            var array = new ArrayVariable("A", indexes);
            var dim = new Dim(array);
            var rte = MakeRunTimeEnvironment();
            dim.Execute(rte);

            var actual = rte.Variables["A"];

            Assert.IsInstanceOfType(actual, typeof(object[,]));
        }

        [TestMethod]
        public void Execute_OfDim_SetsWidht()
        {
            var indexes = new[] { new Constant(100), new Constant(200) };
            var array = new ArrayVariable("A", indexes);
            var dim = new Dim(array);
            var rte = MakeRunTimeEnvironment();
            dim.Execute(rte);

            var actual = (object[,])rte.Variables["A"];

            Assert.AreEqual(99, actual.GetUpperBound(0));
        }

        [TestMethod]
        public void Execute_OfDim_SetsHeight()
        {
            var indexes = new[] { new Constant(100), new Constant(200) };
            var array = new ArrayVariable("A", indexes);
            var dim = new Dim(array);
            var rte = MakeRunTimeEnvironment();
            dim.Execute(rte);

            var actual = (object[,])rte.Variables["A"];

            Assert.AreEqual(199, actual.GetUpperBound(1));
        }

        [TestMethod]
        public void ToString_OfDim_Converts()
        {
            var dim = new Dim(new ArrayVariable("A", new[] { new Constant("1") }));

            var actual = dim.ToString();

            Assert.AreEqual("DIM A[1]", actual);
        }
    }
}
