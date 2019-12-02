namespace Basic.Tests.Expressions
{
    using System.Collections.Generic;
    using Basic.Runtime;
    using Basic.Runtime.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ArrayVariableTests : BaseTests
    {
        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetExpression_WhenIndexCountLessThan1_ThrowsKeyNotFoundException()
        {
            var variables = MakeVariables();
            var indexes = new IExpression[0];
            var expression = new ArrayVariable("A", indexes);

            var actual = expression.GetExpression(variables);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetExpression_WhenIndexCountGreaterThan4_ThrowsKeyNotFoundException()
        {
            var variables = MakeVariables();
            var indexes = new[]
            {
                new Constant(1),
                new Constant(2),
                new Constant(3),
                new Constant(4),
                new Constant(5)
            };
            var expression = new ArrayVariable("A", indexes);

            var actual = expression.GetExpression(variables);
        }

        [TestMethod]
        public void GetExpression_With1DArray_ReturnsElement()
        {
            var variables = MakeVariables();
            variables["A"] = new object[3] { 1, 2, 3 };
            var indexes = new[] { new Constant(3) };
            var expression = new ArrayVariable("A", indexes);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(3, actual);
        }

        [TestMethod]
        public void GetExpression_With2DArray_ReturnsElement()
        {
            var variables = MakeVariables();
            variables["A"] = new object[3, 3]
            {
                { 11, 12, 13 },
                { 21, 22, 23 },
                { 31, 32, 33 }
            };
            var indexes = new[] { new Constant(3), new Constant(2) };
            var expression = new ArrayVariable("A", indexes);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(32, actual);
        }

        [TestMethod]
        public void GetExpression_With3DArray_ReturnsElement()
        {
            var variables = MakeVariables();
            variables["A"] = new object[2, 2, 2]
            {
                {
                    { 111, 112 },
                    { 121, 122 },
                },
                {
                    { 211, 212 },
                    { 221, 222 },
                },
            };
            var indexes = new[] { new Constant(2), new Constant(1), new Constant(2) };
            var expression = new ArrayVariable("A", indexes);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(212, actual);
        }

        [TestMethod]
        public void GetExpression_With4DArray_ReturnsElement()
        {
            var variables = MakeVariables();
            variables["A"] = new object[2, 2, 2, 2]
            {
                {
                    {
                        { 1111, 1112 },
                        { 1121, 1122 },
                    },
                    {
                        { 1211, 1212 },
                        { 1221, 1222 },
                    },
                },
                {
                    {
                        { 2111, 2112 },
                        { 2121, 2122 },
                    },
                    {
                        { 2211, 2212 },
                        { 2221, 2222 },
                    },
                },
            };
            var indexes = new[] { new Constant(2), new Constant(2), new Constant(2), new Constant(2) };
            var expression = new ArrayVariable("A", indexes);

            var actual = expression.GetExpression(variables)
                                   .Calculate();

            Assert.AreEqual(2222, actual);
        }

        [TestMethod]
        public void ToString_WithNameAndIndex_ReturnsArrayOperator()
        {
            var expression = new ArrayVariable("A", new[] { new Constant(3) });

            var actual = expression.ToString();

            Assert.AreEqual("A[3]", actual);
        }
    }
}
