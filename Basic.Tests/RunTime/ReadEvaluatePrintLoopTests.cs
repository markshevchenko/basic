namespace Basic.Tests.Runtime
{
    using System;
    using Basic.Parsing;
    using Basic.Runtime;
    using Basic.Runtime.Statements;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ReadEvaluatePrintLoopTests : BaseTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadEvaluatePrintLoop_WithNullRte_ThrowsArgumentNullException()
        {
            RunTimeEnvironment rte = null;
            var parser = new Parser();

            var repl = new ReadEvaluatePrintLoop(rte, parser);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadEvaluatePrintLoop_WithNullParser_ThrowsArgumentNullException()
        {
            RunTimeEnvironment rte = MakeRunTimeEnvironment("PRINT \"Windows\";");
            Parser parser = null;

            var repl = new ReadEvaluatePrintLoop(rte, parser);
        }

        [TestMethod]
        public void TakeStep_WithLetStatement_ReturnsTheResultOfExpression()
        {
            var inputOutput = MakeInputOutput("LET foo = 2.718281828");
            var rte = MakeRunTimeEnvironment(inputOutput);
            var parser = new Parser();
            var repl = new ReadEvaluatePrintLoop(rte, parser);

            repl.TakeStep();
            var actual = inputOutput.OutputStrings[0];

            Assert.AreEqual("2.718281828", actual);
        }

        [TestMethod]
        public void Read_WithPrintStatement_ReturnsParsedPrintStatement()
        {
            var parser = new Parser();
            var rte = MakeRunTimeEnvironment("PRINT \"Windows\";");
            var repl = new ReadEvaluatePrintLoop(rte, parser);

            var result = repl.Read();
            var actual = result.Statement;

            Assert.IsInstanceOfType(actual, typeof(Print));
        }

        [TestMethod]
        public void Evaluate_WhenLineWithoutNumber_RunsStatementImmediately()
        {
            var parser = new Parser();
            var inputOutput = MakeInputOutput();
            var rte = MakeRunTimeEnvironment(inputOutput);
            var repl = new ReadEvaluatePrintLoop(rte, parser);
            var line = new Line(MakeStatement(() => inputOutput.Write("Windows")));

            var result = repl.Evaluate(line);

            Assert.AreEqual("Windows", inputOutput.OutputStrings[0]);
        }

        [TestMethod]
        public void Evalate_WhenLineWithNumbered_AddsItToLines()
        {
            var parser = new Parser();
            var rte = MakeRunTimeEnvironment();
            var repl = new ReadEvaluatePrintLoop(rte, parser);
            var line = new Line("10", MakeStatement());

            var result = repl.Evaluate(line);

            Assert.IsTrue(rte.Lines.Contains(line));
        }

        [TestMethod]
        public void Print_WithNonEmptyResult_PrintsMessage()
        {
            var parser = new Parser();
            var inputOutput = MakeInputOutput();
            var rte = MakeRunTimeEnvironment(inputOutput);
            var repl = new ReadEvaluatePrintLoop(rte, parser);
            var result = new EvaluateResult("message");

            repl.Print(result);

            Assert.AreEqual("message", inputOutput.OutputStrings[0]);
        }

        [TestMethod]
        public void Print_WithEmptyResult_DoesntPrintSomething()
        {
            var parser = new Parser();
            var inputOutput = MakeInputOutput();
            var rte = MakeRunTimeEnvironment(inputOutput);
            var repl = new ReadEvaluatePrintLoop(rte, parser);

            inputOutput.Write("still message");
            repl.Print(EvaluateResult.None);

            Assert.AreEqual("still message", inputOutput.OutputStrings[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DoEvaluateImmediately_WithNull_ThrowsArgumentNullException()
        {
            var parser = new Parser();
            var inputOutput = MakeInputOutput();
            var rte = MakeRunTimeEnvironment(inputOutput);
            var repl = new ReadEvaluatePrintLoop(rte, parser);

            var condition = repl.DoEvaluateImmediately(null);
        }

        [TestMethod]
        [ExpectedException(typeof(RunTimeException))]
        public void Evaluate_WithExceptedStatement_ThrowsRunTimeException()
        {
            var parser = new Parser();
            var inputOutput = MakeInputOutput();
            var rte = MakeRunTimeEnvironment(inputOutput);
            var repl = new ReadEvaluatePrintLoop(rte, parser);

            var statement = MakeStatement(() => { throw new Exception(); });

            repl.Evaluate(statement);

            var condition = repl.DoEvaluateImmediately(null);
        }

        [TestMethod]
        public void Evaluate_WithResultedStatement_ReturnsResult()
        {
            var parser = new Parser();
            var inputOutput = MakeInputOutput();
            var rte = MakeRunTimeEnvironment(inputOutput);
            var repl = new ReadEvaluatePrintLoop(rte, parser);

            var statement = MakeStatement(new EvaluateResult("message"));

            var result = repl.Evaluate(statement);

            Assert.AreEqual("message", result.Message);
        }

        [TestMethod]
        public void IsOver_AfterCreation_IsFalse()
        {
            var parser = new Parser();
            var rte = MakeRunTimeEnvironment("PRINT \"Windows\";");
            var repl = new ReadEvaluatePrintLoop(rte, parser);

            Assert.IsFalse(repl.IsOver);
        }

        [TestMethod]
        public void IsOver_WhenRteClosed_IsTrue()
        {
            var parser = new Parser();
            var rte = MakeRunTimeEnvironment("PRINT \"Windows\";");
            var repl = new ReadEvaluatePrintLoop(rte, parser);

            rte.Close();

            Assert.IsTrue(repl.IsOver);
        }
    }
}
