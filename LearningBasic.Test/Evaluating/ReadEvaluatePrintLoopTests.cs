namespace LearningBasic.Test.Evaluating
{
    using LearningBasic.Evaluating;
    using LearningBasic.Parsing.Ast;
    using LearningBasic.Parsing.Ast.Statements;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ReadEvaluatePrintLoopTests : BaseTests
    {
        [TestMethod]
        public void Read_WithPrintStatement_ReturnsParsedPrintStatement()
        {
            var parser = MakeParser();
            var rte = MakeRunTimeEnvironment("PRINT \"Windows\";");
            var repl = new ReadEvaluatePrintLoop(rte, parser);

            var result = repl.Read();
            var actual = result.Statement;

            Assert.IsInstanceOfType(actual, typeof(Print));
        }

        [TestMethod]
        public void Evaluate_WithPrintStatement_PrintsArgument()
        {
            var parser = MakeParser();
            var inputOutput = MakeInputOutput();
            var rte = MakeRunTimeEnvironment(inputOutput);
            var repl = new ReadEvaluatePrintLoop(rte, parser);
            var line = new Line(MakePrintStatement("Windows"));

            var result = repl.Evaluate(line);

            Assert.AreEqual("Windows", inputOutput.LastWritten);
        }

        [TestMethod]
        public void Evalate_WithNumberedPrintStatement_StoresStatement()
        {
            var parser = MakeParser();
            var rte = MakeRunTimeEnvironment();
            var repl = new ReadEvaluatePrintLoop(rte, parser);
            var line = new Line("10", MakePrintStatement("Windows"));

            var result = repl.Evaluate(line);

            Assert.IsTrue(rte.Lines.ContainsKey(10));
        }

        [TestMethod]
        public void Print_WithNonEmptyResult_PrintsMessage()
        {
            var parser = MakeParser();
            var inputOutput = MakeInputOutput();
            var rte = MakeRunTimeEnvironment(inputOutput);
            var repl = new ReadEvaluatePrintLoop(rte, parser);
            var result = new EvaluateResult("message");

            repl.Print(result);

            Assert.AreEqual("message\n", inputOutput.LastWritten);
        }

        [TestMethod]
        public void Print_WithEmptyResult_DoesntPrintSomething()
        {
            var parser = MakeParser();
            var inputOutput = MakeInputOutput();
            var rte = MakeRunTimeEnvironment(inputOutput);
            var repl = new ReadEvaluatePrintLoop(rte, parser);

            inputOutput.Write("still message");
            repl.Print(EvaluateResult.Empty);

            Assert.AreEqual("still message", inputOutput.LastWritten);

        }
    }
}
