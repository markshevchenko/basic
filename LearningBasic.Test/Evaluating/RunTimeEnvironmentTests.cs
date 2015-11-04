namespace LearningBasic.Test.Evaluating
{
    using System;
    using LearningBasic.Evaluating;
    using LearningBasic.Parsing.Ast.Expressions;
    using LearningBasic.Parsing.Ast.Statements;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RunTimeEnvironmentTests : BaseTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RunTimeEnvironment_WithNullInputOutput_ThrowsArgumentNullException()
        {
            var inputOutput = (IInputOutput)null;
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RunTimeEnvironment_WithNullProgramRepository_ThrowsArgumentNullException()
        {
            var inputOutput = MakeInputOutput();
            var programRepository = (IProgramRepository)null;
            var rte = new RunTimeEnvironment(inputOutput, programRepository);
        }

        [TestMethod]
        public void RunTimeEnvironment_AfterConstructing_IsNotClosed()
        {
            var inputOutput = MakeInputOutput("any string");
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);

            Assert.IsFalse(rte.IsClosed);
        }

        [TestMethod]
        public void Close_WhenCalled_SetsIsClosedProperty()
        {
            var inputOutput = MakeInputOutput("any string");
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);

            rte.Close();

            Assert.IsTrue(rte.IsClosed);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Save_WithNullName_ThrowsArgumentNullException()
        {
            var inputOutput = MakeInputOutput();
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);
            rte.Lines.Add(10, new End());

            rte.Save(null);
        }

        [TestMethod]
        public void Save_WithNotNullName_PassesNameToProgramRepository()
        {
            var inputOutput = MakeInputOutput();
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);
            rte.Lines.Add(10, new End());

            rte.Save("the name of the file");

            Assert.AreEqual("the name of the file", programRepository.LastFileName);
        }

        [TestMethod]
        public void Save_WithNotNullName_SetsLastUsedName()
        {
            var inputOutput = MakeInputOutput();
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);
            rte.Lines.Add(10, new End());

            rte.Save("the name of the file");

            Assert.AreEqual("the name of the file", rte.LastUsedName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Load_WithNullName_ThrowsArgumentNullException()
        {
            var inputOutput = MakeInputOutput();
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);

            rte.Load(null);
        }

        [TestMethod]
        public void Load_WithNotNullName_PassesNameToProgramRepository()
        {
            var inputOutput = MakeInputOutput();
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);
            rte.Lines.Add(10, new End());

            rte.Load("the name of the file");

            Assert.AreEqual("the name of the file", programRepository.LastFileName);
        }

        [TestMethod]
        public void Load_WithNotNullName_SetsLastUsedName()
        {
            var inputOutput = MakeInputOutput();
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);

            rte.Load("the name of the file");

            Assert.AreEqual("the name of the file", rte.LastUsedName);
        }

        [TestMethod]
        public void Run_WhenNonEmptyLines_EvaluatesFirstStatementOfLines()
        {
            var inputOutput = MakeInputOutput();
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);

            var wasRunned = false;
            var statement = MakeStatement(() => { wasRunned = true; });
            rte.Lines.Add(10, statement);

            var result = rte.Run();

            Assert.IsTrue(wasRunned);
        }

        [TestMethod]
        public void Run_WhenEmptyLines_DoesNotThrowException()
        {
            var inputOutput = MakeInputOutput();
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);

            try
            {
                var result = rte.Run();
            }
            catch (Exception exception)
            {
                Assert.Fail("Expected no exception, but got: " + exception.Message);
            }
        }

        [TestMethod]
        public void Run_WhenIsRunning_AbortsRunning()
        {
            var inputOutput = MakeInputOutput();
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);
            rte.Lines.Add(10, new Run());

            var result = rte.Run();

            Assert.IsTrue(result.IsAborted);
        }

        [TestMethod]
        public void End_WhenIsRunning_StopsRunning()
        {
            var inputOutput = MakeInputOutput();
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);

            rte.Lines.Add(10, new End());
            var shouldNotBeTrue = false;
            rte.Lines.Add(20, MakeStatement(() => { shouldNotBeTrue = true; }));

            var result = rte.Run();

            Assert.IsFalse(shouldNotBeTrue);
        }

        [TestMethod]
        [ExpectedException(typeof(RunTimeException))]
        public void End_WhenIsNotRunning_ThrowsRunTimeException()
        {
            var inputOutput = MakeInputOutput();
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);

            var end = new End();

            var result = end.Execute(rte);
        }

        [TestMethod]
        [ExpectedException(typeof(RunTimeException))]
        public void Goto_WhenIsNotRunning_ThrowsRunTimeException()
        {
            var inputOutput = MakeInputOutput();
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);

            var @goto = new Goto(new Constant("100"));

            var result = @goto.Execute(rte);
        }

        [TestMethod]
        public void Goto_WithNonExistentNumber_AbortsRunning()
        {
            var inputOutput = MakeInputOutput();
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);
            rte.Lines.Add(10, new Goto(new Constant("20")));

            var result = rte.Run();

            Assert.IsTrue(result.IsAborted);
        }

        [TestMethod]
        public void Goto_WithExistentNumber_GoesToSpecifiedLine()
        {
            var inputOutput = MakeInputOutput();
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);
            rte.Lines.Add(10, new Goto(new Constant("30")));
            int actual = 0;
            rte.Lines.Add(20, MakeStatement(() => { actual += 20; }));
            rte.Lines.Add(30, MakeStatement(() => { actual += 30; }));

            var result = rte.Run();

            Assert.AreEqual(30, actual);
        }
    }
}
