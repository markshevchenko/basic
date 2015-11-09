namespace LearningBasic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using LearningBasic.IO;
    using LearningBasic.RunTime;
    using LearningBasic.Parsing;
    using LearningBasic.Parsing.Basic;
    using LearningBasic.Parsing.Code.Expressions;
    using LearningBasic.Parsing.Code.Statements;
    using LearningBasic.Mocks;

    public abstract class BaseTests
    {
        protected static TextReader MakeReader(string inputString)
        {
            return new StringReader(inputString);
        }

        protected static BasicScanner MakeScanner(string inputString)
        {
            var reader = MakeReader(inputString);

            return new BasicScanner(reader);
        }

        protected static BasicParser MakeParser()
        {
            return new BasicParser(new BasicScannerFactory());
        }

        protected static MockInputOutput MakeInputOutput()
        {
            return new MockInputOutput();
        }

        protected static MockInputOutput MakeInputOutput(string inputString)
        {
            return new MockInputOutput(inputString);
        }

        private static IReadOnlyList<ILine> lines = new List<ILine>
        {
            new Line("10", new Input(new ScalarVariable("A"))),
            new Line("20", new Let(new ScalarVariable("B"), new ScalarVariable("A"))),
            new Line("30", new Print(new[] { new ScalarVariable("B") })),
        };

        protected static MockProgramRepository MakeProgramRepository()
        {
            return new MockProgramRepository(lines);
        }

        protected static RunTimeEnvironment MakeRunTimeEnvironment()
        {
            var inputOutput = MakeInputOutput();
            var programRepository = MakeProgramRepository();
            return new RunTimeEnvironment(inputOutput, programRepository);
        }

        protected static RunTimeEnvironment MakeRunTimeEnvironment(string inputString)
        {
            var inputOutput = MakeInputOutput(inputString);
            var programRepository = MakeProgramRepository();
            return new RunTimeEnvironment(inputOutput, programRepository);
        }

        protected static RunTimeEnvironment MakeRunTimeEnvironment(IInputOutput inputOutput)
        {
            var programRepository = MakeProgramRepository();
            return new RunTimeEnvironment(inputOutput, programRepository);
        }

        protected static MockStatement MakeStatement()
        {
            return new MockStatement();
        }

        protected static MockStatement MakeStatement(Action action)
        {
            return new MockStatement(action);
        }
    }
}
