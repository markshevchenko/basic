namespace Basic.Tests.Statements
{
    using System.Linq;
    using Basic.Runtime;
    using Basic.Runtime.Statements;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SaveTests : BaseTests
    {
        [TestMethod]
        public void Execute_OfSave_SavesLines()
        {
            var programRepository = MakeProgramRepository();
            var rte = MakeRunTimeEnvironment(programRepository);
            rte.AddOrUpdate(new Line("10", new Nop()));
            rte.AddOrUpdate(new Line("20", new Nop()));
            rte.AddOrUpdate(new Line("30", new Nop()));
            var load = new Save("filename");

            load.Execute(rte);
            var expected = programRepository.Lines.ToList();
            var actual = rte.Lines;

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Execute_OfSave_UsesFilename()
        {
            var programRepository = MakeProgramRepository();
            var rte = MakeRunTimeEnvironment(programRepository);
            var load = new Load("filename");

            load.Execute(rte);

            Assert.AreEqual("filename", programRepository.LastFileName);
        }

        [TestMethod]
        public void Execute_OfSave_StoresFilename()
        {
            var programRepository = MakeProgramRepository();
            var rte = MakeRunTimeEnvironment(programRepository);
            var load = new Load("filename");

            load.Execute(rte);

            Assert.AreEqual("filename", rte.Variables.LastUsedProgramName);
        }

        [TestMethod]
        public void Execute_OfLoad_Converts()
        {
            var save = new Save("Abc");

            var actual = save.ToString();

            Assert.AreEqual("SAVE \"Abc\"", actual);
        }
    }
}
