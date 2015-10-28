namespace Basic
{
    using System;

    /// <summary>
    /// Implements the READ-EVALUATE-PRINT-LOOP pattern.
    /// </summary>
    public class Repl<TTag>
        where TTag : struct
    {
        private readonly IRunTimeEnvironment runtimeSystem;

        private readonly IParser<TTag> parser;

        private readonly ICommandBuilder<TTag> commandBuilder;

        public Repl(IRunTimeEnvironment runtimeSystem, IParser<TTag> parser, ICommandBuilder<TTag> commandBuilder)
        {
            if (runtimeSystem == null)
                throw new ArgumentNullException("runtimeSystem");

            if (parser == null)
                throw new ArgumentNullException("parser");

            if (commandBuilder == null)
                throw new ArgumentNullException("commandBuilder");

            this.runtimeSystem = runtimeSystem;
            this.parser = parser;
            this.commandBuilder = commandBuilder;
        }

        public void Run()
        {
            while (!runtimeSystem.IsTerminated)
            {
                try
                {
                    RunStep();
                }
                catch (Exception exception)
                {
                    runtimeSystem.InputOutput.WriteLine(exception.ToString());
                    break;
                }
            }
        }

        internal void RunStep()
        {
            var programLine = runtimeSystem.InputOutput.ReadLine();
            var ast = parser.Parse(programLine);
            var lineNumber = ast.Text;
            var statement = ast.Children[0];
            var command = commandBuilder.Build(statement);

            if (string.IsNullOrEmpty(lineNumber))
                command(runtimeSystem);
            else
                runtimeSystem.AddCommand(int.Parse(lineNumber), command);
        }
    }
}
