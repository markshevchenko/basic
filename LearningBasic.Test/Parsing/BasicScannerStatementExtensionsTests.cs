namespace LearningBasic.Test.Parsing
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using LearningBasic.Parsing;
    using LearningBasic.Parsing.Ast;
    using LearningBasic.Parsing.Ast.Statements;

    [TestClass]
    public class BasicScannerStatementExtensionsTests : BaseTests
    {
        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void ReadStatementExcludingNext_WithNext_ThrowsParserException()
        {
            var scanner = MakeScanner("NEXT");

            var value = scanner.ReadStatementExcludingNext();
        }

        [TestMethod]
        public void ReadStatementExcludingNext_WithQuit_ReturnsQuit()
        {
            var scanner = MakeScanner("QUIT");

            var value = scanner.ReadStatementExcludingNext();

            Assert.IsInstanceOfType(value, typeof(Quit));
        }

        [TestMethod]
        public void TryReadLet_WithLet_ReturnsTrue()
        {
            var scanner = MakeScanner("LET a = 100");
            IStatement result;
            var condition = scanner.TryReadLet(out result);

            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void TryReadLet_WithLetWithoutKeyword_ReturnsTrue()
        {
            var scanner = MakeScanner("a = 100");
            IStatement result;
            var condition = scanner.TryReadLet(out result);

            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void TryReadLet_WithPrint_ReturnsFalse()
        {
            var scanner = MakeScanner("PRINT 3.14159265");
            IStatement result;
            var condition = scanner.TryReadLet(out result);

            Assert.IsFalse(condition);
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void TryReadLet_WithLetWithoutAssignment_ThrowsParseException()
        {
            var scanner = MakeScanner("LET");
            IStatement result;
            var condition = scanner.TryReadLet(out result);
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void TryReadLet_WithLetWithoutRight_ThrowsParseException()
        {
            var scanner = MakeScanner("LET result =");
            IStatement result;
            var condition = scanner.TryReadLet(out result);
        }

        [TestMethod]
        public void TryReadPrint_WithPrint_SetsPrintLineAsResult()
        {
            var scanner = MakeScanner("PRINT 3.14159265");
            IStatement result;
            var condition = scanner.TryReadPrint(out result);

            Assert.IsInstanceOfType(result, typeof(PrintLine));
        }

        [TestMethod]
        public void TryReadPrint_WithPrintSemicolon_SetsPrintAsResult()
        {
            var scanner = MakeScanner("PRINT 3.14159265;");
            IStatement result;
            var condition = scanner.TryReadPrint(out result);

            Assert.IsInstanceOfType(result, typeof(Print));
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void TryReadInput_WithoutArguments_ThrowsParseException()
        {
            var scanner = MakeScanner("INPUT");
            IStatement result;
            var condition = scanner.TryReadInput(out result);
        }

        [TestMethod]
        public void TryReadInput_WithoutPrompt_SetsNullPrompt()
        {
            var scanner = MakeScanner("INPUT var");
            IStatement result;
            var condition = scanner.TryReadInput(out result);

            Assert.IsNull((result as Input).Prompt);
        }

        [TestMethod]
        public void TryReadInput_WithPrompt_StoresPrompt()
        {
            var scanner = MakeScanner("INPUT \"prompt\", var");
            IStatement result;
            var condition = scanner.TryReadInput(out result);

            Assert.AreEqual("prompt", (result as Input).Prompt);
        }

        [TestMethod]
        public void TryReadList_WithRange_StoresDefinedRange()
        {
            var scanner = MakeScanner("LIST 30-60");
            IStatement result;

            var condition = scanner.TryReadList(out result);
            var range = (result as List).Range;

            Assert.IsTrue(range.IsDefined);
        }

        [TestMethod]
        public void TryReadList_WithoutRange_StoresUndefinedRange()
        {
            var scanner = MakeScanner("LIST");
            IStatement result;

            var condition = scanner.TryReadList(out result);
            var range = (result as List).Range;

            Assert.IsFalse(range.IsDefined);
        }

        [TestMethod]
        public void TryReadRange_WithEmptyString_SetsUndefinedResult()
        {
            var scanner = MakeScanner("");
            Range result;

            var condition = scanner.TryReadRange(out result);

            Assert.IsFalse(result.IsDefined);
        }

        [TestMethod]
        public void TryReadRange_WithEmptyString_ReturnsFalse()
        {
            var scanner = MakeScanner("");
            Range result;

            var condition = scanner.TryReadRange(out result);

            Assert.IsFalse(condition);
        }

        [TestMethod]
        public void TryReadRange_WithRange_ReturnsTrue()
        {
            var scanner = MakeScanner("20-30");
            Range result;

            var condition = scanner.TryReadRange(out result);

            Assert.IsTrue(condition);
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedTokenException))]
        public void TryReadRange_WithIncompleteRange_ThrowsParserException()
        {
            var scanner = MakeScanner("20-");
            Range result;

            var condition = scanner.TryReadRange(out result);
        }
    }
}
