namespace LearningBasic.Test
{
    using System.IO;
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

        protected static IRunTimeEnvironment MakeRunTimeEnvironment()
        {
            var inputOutput = MakeInputOutput();
            return new RunTimeEnvironment(inputOutput);
        }

        protected static IRunTimeEnvironment MakeRunTimeEnvironment(string inputString)
        {
            var inputOutput = MakeInputOutput(inputString);
            return new RunTimeEnvironment(inputOutput);
        }

        protected static IRunTimeEnvironment MakeRunTimeEnvironment(IInputOutput inputOutput)
        {
            return new RunTimeEnvironment(inputOutput);
        }

        protected static IStatement MakePrintStatement(string argument)
        {
            var expression = new Constant(argument);
            return new Print(new[] { expression });
        }
    }
}
