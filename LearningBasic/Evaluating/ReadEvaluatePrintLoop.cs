namespace LearningBasic.Evaluating
{
    using System;
    using LearningBasic.IO;
    using LearningBasic.Parsing;

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
            catch (IOException exception)
            {
                rte.InputOutput.WriteLine("I/O error: {0}", exception.Message);
            }
            catch (ParserException exception)
            {
                rte.InputOutput.WriteLine("Parser error: {0}", exception.Message);
            }
        }

        /// <summary>
        /// Reads and parses the single BASIC line.
        /// </summary>
        /// <returns>The parsed line.</returns>
        public ILine Read()
        {
            var line = rte.InputOutput.ReadLine();
            return parser.Parse(line);
        }

        /// <summary>
        /// Evaluates the parsed line.
        /// </summary>
        /// <remarks>
        /// If the line has a line number, then evaluator stores the statement into
        /// run-time environment; otherwise it runs the statement.</remarks>
        /// <param name="line">The parsed line.</param>
        /// <returns>The <see cref="EvaluateResult">result of evaluate stage</see>.</returns>
        public EvaluateResult Evaluate(ILine line)
        {
            if (line.Number.HasValue)
            {
                rte.Lines[line.Number.Value] = line.Statement;
                return EvaluateResult.Empty;
            }
            else
                return line.Statement.Run(rte);
        }

        /// <summary>
        /// Prints the result of evaluate stage.
        /// </summary>
        /// <remarks>If the result hasn't a message then prints nothing.</remarks>
        /// <param name="result">The result of evaluate stage.</param>
        public void Print(EvaluateResult result)
        {
            if (result.HasMessage)
                rte.InputOutput.WriteLine(result.Message);
        }
    }
}
