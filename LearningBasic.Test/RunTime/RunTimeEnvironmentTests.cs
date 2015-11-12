namespace LearningBasic.RunTime
{
    using System;
    using LearningBasic.IO;
    using LearningBasic.Parsing;
    using LearningBasic.Parsing.Code.Expressions;
    using LearningBasic.Parsing.Code.Statements;
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
            var rte = MakeRunTimeEnvironment();

            Assert.IsFalse(rte.IsClosed);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BinarySearch_WithNullLine_ThrowsArgumentNullException()
        {
            var rte = MakeRunTimeEnvironment();

            rte.BinarySearch(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BinarySearch_WithLineWithoutLabel_ThrowsArgumentException()
        {
            var rte = MakeRunTimeEnvironment();

            rte.BinarySearch(new Line(new End()));
        }

        [TestMethod]
        public void BinarySearch_WithExistingLabel_ReturnsPositiveIndex()
        {
            var rte = MakeRunTimeEnvironment();
            rte.AddOrUpdate(new Line("10", new Nop()));
            rte.AddOrUpdate(new Line("20", new Nop()));
            rte.AddOrUpdate(new Line("30", new Nop()));

            var existingLabelButDifferentStatement = new Line("20", new Print(new[] { new Constant("Message") }));

            var index = rte.BinarySearch(existingLabelButDifferentStatement);

            Assert.AreEqual(1, index);
        }

        [TestMethod]
        public void BinarySearch_WithNonExistingLabel_ReturnsNegativeIndex()
        {
            var rte = MakeRunTimeEnvironment();
            rte.AddOrUpdate(new Line("10", new Nop()));
            rte.AddOrUpdate(new Line("20", new Nop()));
            rte.AddOrUpdate(new Line("30", new Nop()));

            var nonExistingLabelButSameStatement = new Line("15", new Nop());

            var index = rte.BinarySearch(nonExistingLabelButSameStatement);

            Assert.AreEqual(~1, index);
        }

        [TestMethod]
        public void AddOrUpdate_WithNewLine_AddsLine()
        {
            var rte = MakeRunTimeEnvironment();
            Assert.AreEqual(0, rte.Lines.Count);

            rte.AddOrUpdate(new Line("10", new End()));

            Assert.AreEqual(1, rte.Lines.Count);
        }

        [TestMethod]
        public void AddOrUpdate_WithExistingLine_DoesntIncrementCountOfLines()
        {
            var rte = MakeRunTimeEnvironment();
            Assert.AreEqual(0, rte.Lines.Count);

            rte.AddOrUpdate(new Line("10", new End()));
            rte.AddOrUpdate(new Line("10", new Quit()));

            Assert.AreEqual(1, rte.Lines.Count);
        }

        [TestMethod]
        public void AddOrUpdate_WithExistingLine_UpdatesLine()
        {
            var rte = MakeRunTimeEnvironment();
            Assert.AreEqual(0, rte.Lines.Count);

            rte.AddOrUpdate(new Line("10", new End()));
            rte.AddOrUpdate(new Line("10", new Quit()));
            var statement = rte.Lines[0].Statement;

            Assert.IsInstanceOfType(statement, typeof(Quit));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddOrUpdate_WithNullLine_ThrowsArgumentNullException()
        {
            var rte = MakeRunTimeEnvironment();

            rte.AddOrUpdate(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddOrUpdate_WithLineWithoutLabel_ThrowsArgumentException()
        {
            var rte = MakeRunTimeEnvironment();

            rte.AddOrUpdate(new Line(new End()));
        }

        [TestMethod]
        public void Close_WhenCalled_SetsIsClosedProperty()
        {
            var rte = MakeRunTimeEnvironment();

            rte.Close();

            Assert.IsTrue(rte.IsClosed);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "Double Dispose cause this is the test for double Dispose.")]
        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void Close_AfterDispose_ThrowsObjectDisposedException()
        {
            var rte = MakeRunTimeEnvironment();

            rte.Dispose();
            rte.Close();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Save_WithNullName_ThrowsArgumentNullException()
        {
            var rte = MakeRunTimeEnvironment();
            rte.Lines.Add(new Line("10", new End()));

            rte.Save(null);
        }

        [TestMethod]
        public void Save_WithNotNullName_PassesNameToProgramRepository()
        {
            var programRepository = MakeProgramRepository();
            var rte = MakeRunTimeEnvironment(programRepository);
            rte.Lines.Add(new Line("10", new End()));

            rte.Save("the name of the file");

            Assert.AreEqual("the name of the file", programRepository.LastFileName);
        }

        [TestMethod]
        public void Save_WithNotNullName_SetsLastUsedName()
        {
            var rte = MakeRunTimeEnvironment();
            rte.Lines.Add(new Line("10", new End()));

            rte.Save("the name of the file");

            Assert.AreEqual("the name of the file", rte.LastUsedName);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void Save_AfterDispose_ThrowsObjectDisposedException()
        {
            var rte = MakeRunTimeEnvironment();
            rte.Lines.Add(new Line("10", new End()));

            rte.Dispose();
            rte.Save("file name");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Load_WithNullName_ThrowsArgumentNullException()
        {
            var rte = MakeRunTimeEnvironment();

            rte.Load(null);
        }

        [TestMethod]
        public void Load_WithNotNullName_PassesNameToProgramRepository()
        {
            var programRepository = MakeProgramRepository();
            var rte = MakeRunTimeEnvironment(programRepository);

            rte.Load("the name of the file");

            Assert.AreEqual("the name of the file", programRepository.LastFileName);
        }

        [TestMethod]
        public void Load_WithNotNullName_SetsLastUsedName()
        {
            var rte = MakeRunTimeEnvironment();

            rte.Load("the name of the file");

            Assert.AreEqual("the name of the file", rte.LastUsedName);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void Load_AfterDispose_ThrowsObjectDisposedException()
        {
            var rte = MakeRunTimeEnvironment();

            rte.Dispose();
            rte.Load("file name");
        }

        [TestMethod]
        public void Run_WhenNonEmptyLines_EvaluatesFirstStatementOfLines()
        {
            var rte = MakeRunTimeEnvironment();

            var wasRunned = false;
            var statement = MakeStatement(() => { wasRunned = true; });
            rte.Lines.Add(new Line("10", statement));

            var result = rte.Run();

            Assert.IsTrue(wasRunned);
        }

        [TestMethod]
        public void Run_WhenEmptyLines_DoesNotThrowException()
        {
            var rte = MakeRunTimeEnvironment();

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
        [ExpectedException(typeof(InvalidOperationException))]
        public void Run_WhenIsRunning_ThrowsInvalidOperationException()
        {
            var rte = MakeRunTimeEnvironment();
            rte.Lines.Add(new Line("10", new Run()));

            var result = rte.Run();
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void Run_AfterDispose_ThrowsObjectDisposedException()
        {
            var rte = MakeRunTimeEnvironment();
            rte.Lines.Add(new Line("10", new End()));

            rte.Dispose();
            rte.Run();
        }

        [TestMethod]
        public void End_WhenIsRunning_StopsRunning()
        {
            var rte = MakeRunTimeEnvironment();

            rte.Lines.Add(new Line("10", new End()));
            var shouldNotBeTrue = false;
            rte.Lines.Add(new Line("20", MakeStatement(() => { shouldNotBeTrue = true; })));

            var result = rte.Run();

            Assert.IsFalse(shouldNotBeTrue);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void End_WhenIsNotRunning_ThrowsInvalidOperationException()
        {
            var rte = MakeRunTimeEnvironment();

            var end = new End();

            var result = end.Execute(rte);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void End_AfterDispose_ThrowsObjectDisposedException()
        {
            var rte = MakeRunTimeEnvironment();
            rte.Lines.Add(new Line("10", new End()));

            rte.Dispose();
            rte.End();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Goto_WhenIsNotRunning_ThrowsInvalidOperationException()
        {
            var rte = MakeRunTimeEnvironment();

            var @goto = new Goto(new Constant("100"));

            var result = @goto.Execute(rte);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Goto_WithNonExistentNumber_ThrowsArgumentOutOfRangeException()
        {
            var rte = MakeRunTimeEnvironment();
            rte.Lines.Add(new Line("10", new Goto(new Constant("20"))));

            var result = rte.Run();
        }

        [TestMethod]
        public void Goto_WithExistentNumber_GoesToSpecifiedLine()
        {
            var rte = MakeRunTimeEnvironment();
            rte.Lines.Add(new Line("10", new Goto(new Constant("30"))));
            int actual = 0;
            rte.Lines.Add(new Line("20", MakeStatement(() => { actual += 20; })));
            rte.Lines.Add(new Line("30", MakeStatement(() => { actual += 30; })));

            var result = rte.Run();

            Assert.AreEqual(30, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void Goto_AfterDispose_ThrowsObjectDisposedException()
        {
            var rte = MakeRunTimeEnvironment();
            rte.Lines.Add(new Line("10", new End()));

            rte.Dispose();
            rte.Goto("10");
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void Randomize_AfterDispose_ThrowsObjectDisposedException()
        {
            var rte = MakeRunTimeEnvironment();

            rte.Dispose();
            rte.Randomize(100);
        }

        [TestMethod]
        public void Randomize_WithSeed_CreatesNewRandomObject()
        {
            var rte = MakeRunTimeEnvironment();

            var notExpected = rte.Variables[RunTimeEnvironment.RandomKey];

            const int randomInteger = 54321;
            rte.Randomize(randomInteger);

            var actual = rte.Variables[RunTimeEnvironment.RandomKey];

            Assert.AreNotEqual(notExpected, actual);

            Assert.AreEqual(54321, randomInteger);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void StartLoop_AfterDispose_ThrowsObjectDisposedException()
        {
            var rte = MakeRunTimeEnvironment();

            rte.Dispose();
            rte.StartLoop(MakeLoop(3));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void StartLoop_WhenIsNotRunning_ThrowsInvalidOperationException()
        {
            var rte = MakeRunTimeEnvironment();

            rte.StartLoop(MakeLoop(3));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StartLoop_WhenNullLoop_ThrowsArgumentNullException()
        {
            var rte = MakeRunTimeEnvironment();
            rte.StartRun();

            rte.StartLoop(null);
        }

        [TestMethod]
        public void IsNewLoop_WithNewLoop_ReturnsTrue()
        {
            var rteLoop = new RteLoop(3);

            var condition = rteLoop.Rte.IsNewLoop;

            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void IsNewLoop_WithExistingLoop_ReturnsFalse()
        {
            var rteLoop = new RteLoop(3);

            rteLoop.Rte.StartLoop(rteLoop.Loop);
            var condition = rteLoop.Rte.IsNewLoop;

            Assert.IsFalse(condition);
        }

        [TestMethod]
        public void IsLastLoopOver_AtStartOfLoop_IsFalse()
        {
            var rteLoop = new RteLoop(3);
            rteLoop.Rte.StartLoop(rteLoop.Loop);

            Assert.IsFalse(rteLoop.Rte.IsLastLoopOver);
        }

        [TestMethod]
        public void IsLastLoopOver_AtEndOfLoop_IsTrue()
        {
            var rteLoop = new RteLoop(3);
            rteLoop.Rte.StartLoop(rteLoop.Loop);

            // The count of steps must match with parameter of RteLoop constructor.
            rteLoop.Rte.TakeLastLoopStep();
            rteLoop.Rte.TakeLastLoopStep();
            rteLoop.Rte.TakeLastLoopStep();

            Assert.IsTrue(rteLoop.Rte.IsLastLoopOver);
        }

        [TestMethod]
        public void StopLastLoop_WhenCalled_PopsLoop()
        {
            var rteLoop = new RteLoop(3);
            rteLoop.Rte.StartLoop(rteLoop.Loop);

            rteLoop.Rte.StopLastLoop();

            Assert.AreEqual(0, rteLoop.Rte.StackOfLoops.Count);
        }

        [TestMethod]
        public void GetStartLabelOfLastLoop_WhenCalled_ReturnsLabelOfFirstLine()
        {
            var rteLoop = new RteLoop(3);
            rteLoop.Rte.StartLoop(rteLoop.Loop);

            var startLabel = rteLoop.Rte.GetStartLabelOfLastLoop();

            Assert.AreEqual(RteLoop.FirstLineLabel, startLabel);
        }

        [TestMethod]
        public void InputOutput_OnBreak_WhenCalled_BreaksRunning()
        {
            var rte = MakeRunTimeEnvironment();
            rte.AddOrUpdate(new Line("10", new Nop()));
            rte.AddOrUpdate(new Line("20", new Nop()));
            rte.AddOrUpdate(new Line("30", new Nop()));
            rte.AddOrUpdate(new Line("40", new Nop()));
            rte.StartRun();
            rte.Runner.MoveNext();

            rte.InputOutput_OnBreak(this, EventArgs.Empty);

            Assert.IsTrue(rte.Runner.IsBroke);
        }
    }
}
