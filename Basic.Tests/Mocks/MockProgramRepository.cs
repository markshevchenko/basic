﻿namespace LearningInterpreter.Mocks
{
    using System;
    using System.Collections.Generic;
    using LearningInterpreter.IO;
    using LearningInterpreter.RunTime;

    public class MockProgramRepository : IProgramRepository
    {
        public string LastFileName { get; private set; }

        public IReadOnlyList<ILine> Lines { get; private set; }

        public MockProgramRepository(IReadOnlyList<ILine> lines)
        {
            Lines = lines;
        }

        public IReadOnlyList<ILine> Load(string name)
        {
            LastFileName = name;
            return Lines;
        }

        public void Save(string name, IReadOnlyList<ILine> lines)
        {
            Lines = lines;
            LastFileName = name;
        }
    }
}
