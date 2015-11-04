namespace LearningBasic.Test
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using LearningBasic.Evaluating;
    using LearningBasic.Parsing;
    using LearningBasic.Parsing.Ast.Expressions;
    using LearningBasic.Parsing.Ast.Statements;
    using LearningBasic.Test.Mocks;

    public abstract class BaseTests
    {
        protected static TextReader MakeReader(string inputString)
        {
            return new StringReader(inputString);
        }

        protected static IScanner<Token> MakeScanner(string inputString)
        {
            var reader = MakeReader(inputString);

            return new BasicScanner(reader);
        }

        protected static ILineParser MakeParser()
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

        private static IDictionary<int, IStatement> lines = new Dictionary<int, IStatement>
        {
            { 10, new Input(new ScalarVariable("A")) },
            { 20, new Let(new ScalarVariable("B"), new ScalarVariable("A")) },
            { 30, new Print(new[] { new ScalarVariable("B") }) },
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
