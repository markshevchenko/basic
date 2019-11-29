namespace LearningInterpreter
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using LearningInterpreter.Basic.Code.Statements;
    using LearningInterpreter.Basic.Code.Expressions;
    using LearningInterpreter.Basic.Code;
    using LearningInterpreter.Basic.Parsing;
    using LearningInterpreter.IO;
    using LearningInterpreter.Mocks;
    using LearningInterpreter.RunTime;
    using LearningInterpreter.Parsing;

    /// <summary>
    /// Implements helper methods for unit tests.
    /// </summary>
    public abstract class BaseTests
    {
        protected static TextReader MakeReader(string inputString)
        {
            return new StringReader(inputString);
        }

        protected static Scanner MakeScanner(string inputString)
        {
            var reader = MakeReader(inputString);

            return new Scanner(reader);
        }

        protected static Parser MakeParser()
        {
            return new Parser(new ScannerFactory());
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

        protected static RunTimeEnvironment MakeRunTimeEnvironment(IProgramRepository programRepository)
        {
            return new RunTimeEnvironment(MakeInputOutput(), programRepository);
        }

        protected static MockStatement MakeStatement()
        {
            return new MockStatement();
        }

        protected static MockStatement MakeStatement(Action action)
        {
            return new MockStatement(action);
        }

        protected static MockStatement MakeStatement(EvaluateResult result)
        {
            return new MockStatement(result);
        }

        protected static IExpression MakeExpression()
        {
            return new Constant("123");
        }

        protected static Variables MakeVariables()
        {
            return new Variables();
        }

        protected static MockLoop MakeLoop(int counter)
        {
            return new MockLoop(counter);
        }
    }
}
