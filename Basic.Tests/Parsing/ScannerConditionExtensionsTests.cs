﻿namespace Basic.Tests.Parsing
{
    using Basic.Parsing;
    using Basic.Runtime.Conditions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ScannerConditionExtensionsTests : BaseTests
    {
        [TestMethod]
        public void ReadCondition_WithOrXor_PlacesXorToRoot()
        {
            var scanner = new Scanner("a = a or b = b xor c = c");

            var value = scanner.ReadCondition();

            Assert.IsInstanceOfType(value, typeof(Xor));
        }

        [TestMethod]
        public void ReadCondition_WithOrXor_PlacesOrToLeftChild()
        {
            var scanner = new Scanner("a = a or b = b xor c = c");

            var value = scanner.ReadCondition();
            value = (value as Xor).Left;

            Assert.IsInstanceOfType(value, typeof(Or));
        }

        [TestMethod]
        public void ReadCondition_WithOrAnd_PlacesOrToRoot()
        {
            var scanner = new Scanner("a = a or b = b and c = c");

            var value = scanner.ReadCondition();

            Assert.IsInstanceOfType(value, typeof(Or));
        }

        [TestMethod]
        public void ReadOrOperand_WithAndAnd_PlacesSecondAndToRoot()
        {
            var scanner = new Scanner("a > a and b = b and c < c");

            var value = scanner.ReadOrOperand();
            value = (value as And).Right;

            Assert.IsInstanceOfType(value, typeof(LessThan));
        }

        [TestMethod]
        public void ReadAndOperand_WithNotRelation_CreatesNotNode()
        {
            var scanner = new Scanner("not a = a");

            var value = scanner.ReadAndOperand();

            Assert.IsInstanceOfType(value, typeof(Not));
        }

        [TestMethod]
        public void ReadAndOperand_WithRelationInParentheses_ReturnsRelation()
        {
            var scanner = new Scanner("(a > 2)");

            var value = scanner.ReadAndOperand();

            Assert.IsInstanceOfType(value, typeof(GreaterThan));
        }

        [TestMethod]
        public void ReadRelation_WithDoubleRelation_ReadsFirstRelationOnly()
        {
            var scanner = new Scanner("a > 2 > 3");

            var value = scanner.ReadRelation();
            var actual = scanner.CurrentToken;

            Assert.AreEqual(Token.Gt, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void ReadRelation_WithAdd_ThrowsParserException()
        {
            var scanner = new Scanner("a + 3");

            var value = scanner.ReadRelation();
        }
    }
}
