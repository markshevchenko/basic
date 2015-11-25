namespace LearningInterpreter.RunTime
{
    using System;
    using LearningInterpreter.IO;
    using LearningInterpreter.Parsing;

    /// <summary>
    /// Implements the Read-Evaluate-Print step-by-step loop.
    /// </summary>
    public class ReadEvaluatePrintLoop : ILoop
    {
        private readonly IRunTimeEnvironment rte;
        private readonly ILineParser parser;
        private readonly ICompiler compiler;

        /// <summary>
        /// Retrieves a <c>Boolean</c> value that indicates whether the specified loop has been terminated.
        /// </summary>
        public virtual bool IsOver { get { return rte.IsClosed; } }

        /// <summary>
        /// Creates an instance of Read-Evaluate-Print loop object.
        /// </summary>
        /// <param name="rte"></param>
        /// <param name="parser"></param>
        public ReadEvaluatePrintLoop(IRunTimeEnvironment rte, ILineParser parser, ICompiler compiler)
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
        public virtual IAstNode Read()
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
        /// <param name="node">The parsed line.</param>
        /// <returns>The <see cref="EvaluateResult">result of the evaluating stage</see>.</returns>
        public virtual EvaluateResult Evaluate(IAstNode node)
        {
            var function = compiler.Compile(node);

            var result = function(rte);

            return result;
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
