namespace LearningBasic
{
    using System;

    /// <summary>
    /// Implements the Read-Eval-Print step-by-step loop.
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
        /// 
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

        public ILine Read()
        {
            var line = rte.InputOutput.ReadLine();
            return parser.Parse(line);
        }

        public Result Evaluate(ILine line)
        {
            if (line.Number.HasValue)
            {
                rte.Statements[line.Number.Value] = line.Statement;
                return new Result(line.Statement);
            }
            else
                return line.Statement.Run(rte);
        }

        public void Print(Result result)
        {
            if (result.HasValue)
                rte.InputOutput.WriteLine(result.Value);
        }
    }
}
