namespace LearningInterpreter.RunTime
{
    using System;
    using System.Collections.Generic;
    using LearningInterpreter.Basic.Code;
    using LearningInterpreter.Basic.Code.Expressions;
    using LearningInterpreter.Basic.Code.Statements;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ProgramRunnerTests
    {
        private readonly static IReadOnlyList<ILine> lines = new[]
        {
            new Line("10", new Rem("Fibonacci sequence")),
            new Line("20", new Print(new[] {new Constant("1") })),
            new Line("30", new Print(new[] {new Constant("1") })),
            new Line("40", new Print(new[] {new Constant("2") })),
            new Line("50", new Print(new[] {new Constant("3") })),
            new Line("60", new Print(new[] {new Constant("4") })),
            new Line("70", new Print(new[] {new Constant("8") })),
            new Line("80", new Print(new[] {new Constant("13") })),
            new Line("90", new Print(new[] {new Constant("21") })),
            new Line("100", new Rem("The end")),
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
        public void RunningLine_AfterConstructorCall_ThrowsIndexOutOfRangeException()
        {
            var runner = new ProgramRunner(lines);

            var statement = runner.RunningLine;
        }

        [TestMethod]
        public void RunningLine_AfterMoveNext_RetursFirstLine()
        {
            var runner = new ProgramRunner(lines);
            var condition = runner.MoveNext();

            var statement = runner.RunningLine.Statement;

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
            const string existingNumber = "30";
            runner.Goto(existingNumber);

            var condition = runner.MoveNext();

            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void MoveNext_AfterGoto_KeepsCurrentStatement()
        {
            var runner = new ProgramRunner(lines);
            const string existingNumber = "30";
            runner.Goto(existingNumber);

            var expected = runner.RunningLine;
            var condition = runner.MoveNext();
            var actual = runner.RunningLine;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Goto_WithExistingLineNumber_SetsCurrentStatement()
        {
            var runner = new ProgramRunner(lines);
            const string existingNumberWithRemStatement = "100";

            runner.Goto(existingNumberWithRemStatement);
            var actual = (runner.RunningLine.Statement as Rem).Comment;

            Assert.AreEqual("The end", actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Goto_WithNonExistingLineNumber_ThrowsArgumentOutOfRangeException()
        {
            var runner = new ProgramRunner(lines);
            const string nonExistingNumber = "200";

            runner.Goto(nonExistingNumber);
        }
    }
}
