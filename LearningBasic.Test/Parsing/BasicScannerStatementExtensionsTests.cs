namespace LearningBasic.Test.Parsing
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using LearningBasic.Parsing;
    using LearningBasic.Parsing.Ast;
    using LearningBasic.Parsing.Ast.Conditions;
    using LearningBasic.Parsing.Ast.Expressions;
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
        public void ReadStatementExcludingNext_WithRun_ReturnsRun()
        {
            var scanner = MakeScanner("RUN");

            var value = scanner.ReadStatementExcludingNext();

            Assert.IsInstanceOfType(value, typeof(Run));
        }

        [TestMethod]
        public void ReadStatementExcludingNext_WithEnd_ReturnsEnd()
        {
            var scanner = MakeScanner("END");

            var value = scanner.ReadStatementExcludingNext();

            Assert.IsInstanceOfType(value, typeof(End));
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
        public void TryReadPrint_WithPrint_StoresPrintLineAsResult()
        {
            var scanner = MakeScanner("PRINT 3.14159265");
            IStatement result;
            var condition = scanner.TryReadPrint(out result);

            Assert.IsInstanceOfType(result, typeof(PrintLine));
        }

        [TestMethod]
        public void TryReadPrint_WithPrintSemicolon_StoresPrintAsResult()
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
        public void TryReadInput_WithoutPrompt_StoresNullPrompt()
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
        public void TryReadRange_WithEmptyString_StoresUndefinedResult()
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

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void ReadRange_WithNoRange_ThrowsParserException()
        {
            var scanner = MakeScanner("no range");

            var actual = scanner.ReadRange();
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void TryReadRemove_WithoutRange_ThrowsParserException()
        {
            var scanner = MakeScanner("REMOVE");
            IStatement result;

            var condition = scanner.TryReadRemove(out result);
        }

        [TestMethod]
        public void TryReadRemove_WithRange_StoresRange()
        {
            var scanner = MakeScanner("REMOVE 100-200");
            IStatement result;

            var condition = scanner.TryReadRemove(out result);
            var statement = result as Remove;

            var expected = new Range(100, 200);
            Assert.AreEqual(expected, statement.Range);
        }

        [TestMethod]
        public void TryReadSave_WithFileName_StoresIt()
        {
            var scanner = MakeScanner("SAVE \"very special program\"");
            IStatement result;

            var condition = scanner.TryReadSave(out result);
            var statement = result as Save;

            Assert.AreEqual("very special program", statement.Name);
        }

        [TestMethod]
        public void TryReadSave_WithoutFileName_StoresNullName()
        {
            var scanner = MakeScanner("SAVE");
            IStatement result;

            var condition = scanner.TryReadSave(out result);
            var statement = result as Save;

            Assert.IsNull(statement.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedTokenException))]
        public void TryReadLoad_WithoutFileName_ThrowsParserException()
        {
            var scanner = MakeScanner("LOAD");
            IStatement result;

            var condition = scanner.TryReadLoad(out result);
        }

        [TestMethod]
        public void TryReadGoto_WithNumber_StoresIt()
        {
            var scanner = MakeScanner("GOTO 100");
            IStatement result;

            var condition = scanner.TryReadGoto(out result);
            var actual = ((result as Goto).Number as Constant).Value;

            Assert.AreEqual(100, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void TryReadGoto_WithoutNumber_ThrowsParserException()
        {
            var scanner = MakeScanner("GOTO");
            IStatement result;

            var condition = scanner.TryReadGoto(out result);
        }

        [TestMethod]
        public void TryReadRandomize_WithSeed_StoresIt()
        {
            var scanner = MakeScanner("RANDOMIZE 314159265");
            IStatement result;

            var condition = scanner.TryReadRandomize(out result);
            var actual = ((result as Randomize).Seed as Constant).Value;

            Assert.AreEqual(314159265, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void TryReadRandomize_WithoutSeed_ThrowsParserException()
        {
            var scanner = MakeScanner("RANDOMIZE");
            IStatement result;

            var condition = scanner.TryReadRandomize(out result);
        }

        [TestMethod]
        public void TryReadRem_WithComment_TrimsItLeftAndStoresAsIs()
        {
            var scanner = MakeScanner("REM   non-EMPTY Comment");
            IStatement result;

            var condition = scanner.TryReadRem(out result);
            var actual = (result as Rem).Comment;

            Assert.AreEqual("non-EMPTY Comment", actual);
        }

        [TestMethod]
        public void TryReadRem_WithoutComment_StoresEmptyString()
        {
            var scanner = MakeScanner("REM   ");
            IStatement result;

            var condition = scanner.TryReadRem(out result);
            var actual = (result as Rem).Comment;

            Assert.AreEqual("", actual);
        }

        [TestMethod]
        public void TryReadIfThenElse_WithConditionAndThen_StoresCondition()
        {
            var scanner = MakeScanner("IF a <> 0 THEN PRINT a");
            IStatement result;

            var condition = scanner.TryReadIfThenElse(out result);
            var value = (result as IfThenElse).Condition;

            Assert.IsInstanceOfType(value, typeof(NotEqual));
        }

        [TestMethod]
        public void TryReadIfThenElse_WithConditionAndThen_StoresThen()
        {
            var scanner = MakeScanner("IF a <> 0 THEN PRINT a");
            IStatement result;

            var condition = scanner.TryReadIfThenElse(out result);
            var value = (result as IfThenElse).Then;

            Assert.IsInstanceOfType(value, typeof(Print));
        }

        [TestMethod]
        public void TryReadIfThenElse_WithConditionAndThen_StoresNullElse()
        {
            var scanner = MakeScanner("IF a <> 0 THEN PRINT a");
            IStatement result;

            var condition = scanner.TryReadIfThenElse(out result);
            var value = (result as IfThenElse).Else;

            Assert.IsNull(value);
        }

        [TestMethod]
        public void TryReadIfThenElse_WithConditionThenAndElse_StoresElse()
        {
            var scanner = MakeScanner("IF a <> 0 THEN PRINT a ELSE INPUT a");
            IStatement result;

            var condition = scanner.TryReadIfThenElse(out result);
            var value = (result as IfThenElse).Else;

            Assert.IsInstanceOfType(value, typeof(Input));
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void TryReadIfThenElse_WithInvalidCondition_ThrowsUnexpectedTokenException()
        {
            var scanner = MakeScanner("IF a + b THEN PRINT a");
            IStatement result;

            var condition = scanner.TryReadIfThenElse(out result);
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedTokenException))]
        public void TryReadIfThenElse_WithoutThenKeyword_ThrowsUnexpectedTokenException()
        {
            var scanner = MakeScanner("IF a <> b PRINT a");
            IStatement result;

            var condition = scanner.TryReadIfThenElse(out result);
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void TryReadIfThenElse_WithoutThenStatement_ThrowsUnexpectedTokenException()
        {
            var scanner = MakeScanner("IF a <> b THEN");
            IStatement result;

            var condition = scanner.TryReadIfThenElse(out result);
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void TryReadIfThenElse_WithoutElseStatement_ThrowsUnexpectedTokenException()
        {
            var scanner = MakeScanner("IF a <> b THEN PRINT a ELSE");
            IStatement result;

            var condition = scanner.TryReadIfThenElse(out result);
        }
    }
}
