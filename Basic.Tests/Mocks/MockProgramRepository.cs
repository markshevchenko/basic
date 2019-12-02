namespace Basic.Tests.Mocks
{
    using System.Collections.Generic;
    using Basic.IO;
    using Basic.Runtime;

    public class MockProgramRepository : IProgramRepository
    {
        public string LastFileName { get; private set; }

        public IReadOnlyList<Line> Lines { get; private set; }

        public MockProgramRepository(IReadOnlyList<Line> lines)
        {
            Lines = lines;
        }

        public IReadOnlyList<Line> Load(string name)
        {
            LastFileName = name;
            return Lines;
        }

        public void Save(string name, IReadOnlyList<Line> lines)
        {
            Lines = lines;
            LastFileName = name;
        }
    }
}
