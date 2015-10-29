namespace LearningBasic.Console
{
    using System;

    /// <summary>
    /// Implements the READ-EVALUATE-PRINT-LOOP pattern.
    /// </summary>
    public class ReadEvaluatePrintLoop
    {
        private readonly IRunTimeEnvironment rte;
        private readonly IParser<Tag> parser;
        private readonly IStatementCompiler<Tag> compiler;

        public ReadEvaluatePrintLoop(IRunTimeEnvironment rte, IParser<Tag> parser, IStatementCompiler<Tag> compiler)
        {
            if (rte == null)
                throw new ArgumentNullException("rte");

            if (parser == null)
                throw new ArgumentNullException("parser");

            if (compiler == null)
                throw new ArgumentNullException("compiler");

            this.rte = rte;
            this.parser = parser;
            this.compiler = compiler;
        }

        public void Run()
        {
            while (!rte.IsClosed)
            {
                try
                {
                    Step();
                }
                catch (Exception exception)
                {
                    rte.InputOutput.WriteLine(exception.ToString());
                }
            }
        }

        internal void Step()
        {
            var programLine = rte.InputOutput.ReadLine();
            var line = parser.Parse(programLine);
            var statement = line.Children[0];
            var action = compiler.Compile(statement);

            if (string.IsNullOrEmpty(line.Text))
            {
                action(rte);
                return;
            }

            int lineNumber = int.Parse(line.Text);
            rte.Statements[lineNumber] = new Statement(statement, action);
        }
    }
}
