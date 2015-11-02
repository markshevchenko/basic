namespace LearningBasic.Test.Mocks
{
    using System.Collections.Generic;
    using LearningBasic.IO;

    public class MockProgramRepository : IProgramRepository
    {
        public const string ValidFileName = "valid file name";

        public IDictionary<int, IStatement> Lines { get; private set; }

        public MockProgramRepository(IDictionary<int, IStatement> lines)
        {
            Lines = lines;
        }

        public IDictionary<int, IStatement> Load(string name)
        {
            if (name == ValidFileName)
                return Lines;

            throw new IOException();
        }

        public void Save(string name, IDictionary<int, IStatement> lines)
        {
            if (name == ValidFileName)
                Lines = lines;

            throw new IOException();
        }
    }
}
