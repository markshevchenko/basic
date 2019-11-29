﻿namespace LearningInterpreter
{
    using LearningInterpreter.Basic.Code;
    using LearningInterpreter.Basic.Code.Statements;
    using LearningInterpreter.Mocks;
    using LearningInterpreter.RunTime;

    /// <summary>
    /// Helper object to represent run-time environment with runned loop.
    /// </summary>
    public class RteLoop : BaseTests
    {
        public const string FirstLineLabel = "10";

        public RunTimeEnvironment Rte { get; private set; }

        public MockLoop Loop { get; private set; }

        public RteLoop(int iterationCount)
        {
            Rte = MakeRunTimeEnvironment();
            Rte.AddOrUpdate(new Line(FirstLineLabel, new Nop()));
            Rte.StartRun();
            Rte.Runner.MoveNext();

            Loop = MakeLoop(iterationCount);
        }
    }
}
