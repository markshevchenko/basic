namespace LearningBasic.Test.Parsing
{
    using LearningBasic.Parsing;
    using LearningBasic.Parsing.Ast.Statements;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BasicParserTests : BaseTests
    {
        [TestMethod]
        public void Parse_WithLineNumberWithNext_StoresLineNumber()
        {
            var parser = MakeParser();

            var line = parser.Parse("10 NEXT");

            Assert.AreEqual(10, line.Number);
        }

        [TestMethod]
        public void Parse_WithLineNumberWithNext_StoresNext()
        {
            var parser = MakeParser();

            var line = parser.Parse("10 NEXT");

            Assert.IsInstanceOfType(line.Statement, typeof(Next));
        }

        [TestMethod]
        public void Parse_WithoutLineNumberWithPrint_StoresNullLineNumber()
        {
            var parser = MakeParser();

            var line = parser.Parse("PRINT 3.1415");

            Assert.IsNull(line.Number);
        }

        [TestMethod]
        public void Parse_WithoutLineNumberWithPrint_StoresPrint()
        {
            var parser = MakeParser();

            var line = parser.Parse("PRINT 3.1415");

            Assert.IsInstanceOfType(line.Statement, typeof(Print));
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void Parse_WithoutLineNumberWithNext_ThrowsParserException()
        {
            var parser = MakeParser();

            var line = parser.Parse("NEXT");
        }
    }
}
