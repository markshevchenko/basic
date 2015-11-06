namespace LearningBasic.Test.Evaluating
{
    using System;
    using System.Collections.Generic;
    using LearningBasic.Evaluating;
    using LearningBasic.Parsing.Ast.Expressions;
    using LearningBasic.Parsing.Ast.Statements;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ProgramRunnerTests
    {
        private readonly static IDictionary<int, IStatement> lines = new Dictionary<int, IStatement>
        {
            { 10, new Rem("Fibonacci sequence") },
            { 20, new Print(new[] {new Constant("1") }) },
            { 30, new Print(new[] {new Constant("1") }) },
            { 40, new Print(new[] {new Constant("2") }) },
            { 50, new Print(new[] {new Constant("3") }) },
            { 60, new Print(new[] {new Constant("4") }) },
            { 70, new Print(new[] {new Constant("8") }) },
            { 80, new Print(new[] {new Constant("13") }) },
            { 90, new Print(new[] {new Constant("21") }) },
            { 100, new Rem("The end") },
        };

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ProgramRunner_WithNull_ThrowsArgumentNullException()
        {
            var runner = new ProgramRunner(null);
        }

        [TestMethod]
        public void IsBroke_AfterConstructorCall_IsFalse()
        {
            var runner = new ProgramRunner(lines);

            Assert.IsFalse(runner.IsBroke);
        }

        [TestMethod]
        public void IsBroke_AfterBreak_IsTrue()
        {
            var runner = new ProgramRunner(lines);

            runner.Break();

            Assert.IsTrue(runner.IsBroke);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CurrentStatement_AfterConstructorCall_ThrowsIndexOutOfRangeException()
        {
            var runner = new ProgramRunner(lines);

            var statement = runner.CurrentStatement;
        }

        [TestMethod]
        public void CurrentStatement_AfterMoveNext_RetursFirstLine()
        {
            var runner = new ProgramRunner(lines);
            var condition = runner.MoveNext();

            var statement = runner.CurrentStatement;

            Assert.IsInstanceOfType(statement, typeof(Rem));
        }

        [TestMethod]
        public void MoveNext_AfterConstructorCall_ReturnsTrue()
        {
            var runner = new ProgramRunner(lines);

            var condition = runner.MoveNext();

            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void MoveNext_AfterBreak_ReturnsFalse()
        {
            var runner = new ProgramRunner(lines);
            runner.Break();

            var condition = runner.MoveNext();

            Assert.IsFalse(condition);
        }

        [TestMethod]
        public void MoveNext_AfterComplete_ReturnsFalse()
        {
            var runner = new ProgramRunner(lines);
            runner.Complete();

            var condition = runner.MoveNext();

            Assert.IsFalse(condition);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MoveNext_BeyondTheEndOfProgram_ThrowsInvalidOperationException()
        {
            var runner = new ProgramRunner(lines);
            while (runner.MoveNext())
                ;

            runner.MoveNext();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MoveNext_WhenRecallAfterComplete_ThrowsInvalidOperationException()
        {
            var runner = new ProgramRunner(lines);

            runner.Complete();
            runner.MoveNext();
            runner.MoveNext();
        }

        [TestMethod]
        public void MoveNext_AfterGoto_ReturnsTrue()
        {
            var runner = new ProgramRunner(lines);
            const int existingNumber = 30;
            runner.Goto(existingNumber);

            var condition = runner.MoveNext();

            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void MoveNext_AfterGoto_KeepsCurrentStatement()
        {
            var runner = new ProgramRunner(lines);
            const int existingNumber = 30;
            runner.Goto(existingNumber);

            var expected = runner.CurrentStatement;
            var condition = runner.MoveNext();
            var actual = runner.CurrentStatement;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Goto_WithExistingLineNumber_SetsCurrentStatement()
        {
            var runner = new ProgramRunner(lines);
            const int existingNumberWithRemStatement = 100;

            runner.Goto(existingNumberWithRemStatement);
            var actual = (runner.CurrentStatement as Rem).Comment;

            Assert.AreEqual("The end", actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Goto_WithNonExistingLineNumber_ThrowsArgumentOutOfRangeException()
        {
            var runner = new ProgramRunner(lines);
            const int nonExistingNumber = 200;

            runner.Goto(nonExistingNumber);
        }
    }
}
