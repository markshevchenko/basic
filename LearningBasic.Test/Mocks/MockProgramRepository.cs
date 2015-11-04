namespace LearningBasic.Test.Mocks
{
    using System.Collections.Generic;
    using LearningBasic.IO;

    public class MockProgramRepository : IProgramRepository
    {
        public const string ValidFileName = "valid file name";

        public IDictionary<int, IStatement> Lines { get; private set; }

        public string LastFileName { get; private set; }

        public MockProgramRepository(IDictionary<int, IStatement> lines)
        {
            Lines = lines;
        }

        public IDictionary<int, IStatement> Load(string name)
        {
            LastFileName = name;
            return Lines;
        }

        public void Save(string name, IDictionary<int, IStatement> lines)
        {
            Lines = lines;
            LastFileName = name;
        }
    }
}
