namespace LearningBasic
{
    using System;

    /// <summary>
    /// Implements the Read-Eval-Print step-by-step loop.
    /// </summary>
    /// <typeparam name="TTag">The type of a tag of an Abstract Syntax Tree.</typeparam>
    public class ReadEvaluatePrintLoop<TTag>
        where TTag : struct
    {
        private readonly IRunTimeEnvironment rte;
        private readonly IParser<TTag> parser;
        private readonly ICompiler<TTag> compiler;

        /// <summary>
        /// Retrieves a <c>Boolean</c> value that indicates whether the specified loop has been terminated.
        /// </summary>
        public bool IsTerminated { get { return rte.IsClosed; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rte"></param>
        /// <param name="parser"></param>
        /// <param name="compiler"></param>
        public ReadEvaluatePrintLoop(IRunTimeEnvironment rte, IParser<TTag> parser, ICompiler<TTag> compiler)
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

        public void TakeStep()
        {
            try
            {
                var line = Read();
                var result = Evaluate(line);
                Print(result);
            }
            catch (BasicException exception)
            {
                rte.InputOutput.WriteLine(exception.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Line<TTag> Read()
        {
            var inputLine = rte.InputOutput.ReadLine();
            var parsedLine = parser.Parse(inputLine);
            return new Line<TTag>(parsedLine);
        }

        public string Evaluate(Line<TTag> line)
        {
            var statement = compiler.Compile(line.Statement);

            if (line.Number.HasValue)
            {
                rte.Statements[line.Number.Value] = statement;
                return statement.ToString();
            }
            else
                return statement.Run(rte);
        }

        public void Print(string result)
        {
            if (!string.IsNullOrEmpty(result))
                rte.InputOutput.WriteLine(result);
        }
    }
}
