namespace LearningBasic
{
    using System;

    /// <summary>
    /// Implements the Read-Evaluate-Print step-by-step loop.
    /// </summary>
    public class ReadEvaluatePrintLoop
    {
        private readonly IRunTimeEnvironment rte;
        private readonly ILineParser parser;

        /// <summary>
        /// Retrieves a <c>Boolean</c> value that indicates whether the specified loop has been terminated.
        /// </summary>
        public bool IsTerminated { get { return rte.IsClosed; } }

        /// <summary>
        /// Creates an instance of Read-Evaluate-Print loop object.
        /// </summary>
        /// <param name="rte"></param>
        /// <param name="parser"></param>
        public ReadEvaluatePrintLoop(IRunTimeEnvironment rte, ILineParser parser)
        {
            if (rte == null)
                throw new ArgumentNullException("rte");

            if (parser == null)
                throw new ArgumentNullException("parser");

            this.rte = rte;
            this.parser = parser;
        }

        /// <summary>
        /// Takes next step of the Read-Evaluate-Print loop.
        /// </summary>
        public void TakeStep()
        {
            try
            {
                var line = Read();
                var result = Evaluate(line);
                Print(result);
            }
            catch (ParserException exception)
            {
                rte.InputOutput.WriteLine("Parser error: {0}", exception.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ILine Read()
        {
            var line = rte.InputOutput.ReadLine();
            return parser.Parse(line);
        }

        public StatementResult Evaluate(ILine line)
        {
            if (line.Number.HasValue)
            {
                rte.Lines[line.Number.Value] = line.Statement;
                return StatementResult.Empty;
            }
            else
                return line.Statement.Run(rte);
        }

        public void Print(StatementResult result)
        {
            if (result.HasMessage)
                rte.InputOutput.WriteLine(result.Message);
        }
    }
}
