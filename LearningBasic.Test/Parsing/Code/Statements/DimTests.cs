namespace LearningBasic.Parsing.Code.Statements
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using LearningBasic.Parsing.Code.Expressions;

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
    }
}
