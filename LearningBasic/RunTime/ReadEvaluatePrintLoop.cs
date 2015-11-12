namespace LearningBasic.RunTime
{
    using System;
    using LearningBasic.IO;
    using LearningBasic.Parsing;

    /// <summary>
    /// Implements the Read-Evaluate-Print step-by-step loop.
    /// </summary>
    public class ReadEvaluatePrintLoop : ILoop
    {
        private readonly IRunTimeEnvironment rte;
        private readonly ILineParser parser;

        /// <summary>
        /// Retrieves a <c>Boolean</c> value that indicates whether the specified loop has been terminated.
        /// </summary>
        public virtual bool IsOver { get { return rte.IsClosed; } }

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
        public virtual void TakeStep()
        {
            try
            {
                var line = Read();
                var result = Evaluate(line);
                Print(result);
            }
            catch (RunTimeException exception)
            {
                rte.InputOutput.WriteLine(exception.Message);
            }
            catch (IOException exception)
            {
                rte.InputOutput.WriteLine(exception.Message);
            }
            catch (ParserException exception)
            {
                rte.InputOutput.WriteLine(exception.Message);
            }
        }

        /// <summary>
        /// Reads and parses the single source line.
        /// </summary>
        /// <returns>The parsed line.</returns>
        public virtual ILine Read()
        {
            var line = rte.InputOutput.ReadLine();
            return parser.Parse(line);
        }

        /// <summary>
        /// Evaluates the parsed line.
        /// </summary>
        /// <remarks>
        /// This method evaluates the line immeditaely if the <see cref="ILine.Label"/> is <c>null</c> or empty.
        /// If you want a different strategy, you can override the <see cref="DoEvaluateImmediately(ILine)"/> method in an inheritor class.
        /// </remarks>
        /// <param name="line">The parsed line.</param>
        /// <returns>The <see cref="EvaluateResult">result of the evaluating stage</see>.</returns>
        public virtual EvaluateResult Evaluate(ILine line)
        {
            if (DoEvaluateImmediately(line))
                return Evaluate(line.Statement);

            rte.AddOrUpdate(line);
            return EvaluateResult.None;
        }

        /// <summary>
        /// Detects whether evaluate the <see cref="ILine.Statement">statement</see> immediately.
        /// </summary>
        /// <param name="line">The input line.</param>
        /// <returns><c>true</c> if the statement should be evaluated immeditely, otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// This method takes a decision checking whether the <see cref="ILine.Label"/> is <c>null</c> or empty.
        /// If you want a different strategy, you can override the method in an inheritor class.
        /// </remarks>
        protected internal virtual bool DoEvaluateImmediately(ILine line)
        {
            if (line == null)
                throw new ArgumentNullException("line");

            return string.IsNullOrEmpty(line.Label);
        }

        internal EvaluateResult Evaluate(IStatement statement)
        {
            try
            {
                return statement.Execute(rte);
            }
            catch (Exception exception)
            {
                var message = string.Format(ErrorMessages.RunTimeErrorOccured, exception.Message);
                throw new RunTimeException(message, exception);
            }
        }

        /// <summary>
        /// Prints the result of evaluate stage.
        /// </summary>
        /// <remarks>If the result hasn't a message then prints nothing.</remarks>
        /// <param name="result">The result of evaluate stage.</param>
        public virtual void Print(EvaluateResult result)
        {
            if (result != EvaluateResult.None)
                rte.InputOutput.WriteLine(result.Message);
        }
    }
}
