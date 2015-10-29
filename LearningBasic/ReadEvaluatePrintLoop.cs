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
        private readonly ICodeGenerator<TTag> codeGenerator;
        private readonly ICodeFormatter<TTag> codeFormatter;

        /// <summary>
        /// Retrieves a <c>Boolean</c> value that indicates whether the specified loop has been terminated.
        /// </summary>
        public bool IsTerminated { get { return rte.IsClosed; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rte"></param>
        /// <param name="parser"></param>
        /// <param name="codeGenerator"></param>
        /// <param name="codeFormatter"></param>
        public ReadEvaluatePrintLoop(
            IRunTimeEnvironment rte,
            IParser<TTag> parser,
            ICodeGenerator<TTag> codeGenerator,
            ICodeFormatter<TTag> codeFormatter)
        {
            if (rte == null)
                throw new ArgumentNullException("rte");

            if (parser == null)
                throw new ArgumentNullException("parser");

            if (codeGenerator == null)
                throw new ArgumentNullException("codeGenerator");

            if (codeFormatter == null)
                throw new ArgumentNullException("codeFormatter");

            this.rte = rte;
            this.parser = parser;
            this.codeGenerator = codeGenerator;
            this.codeFormatter = codeFormatter;
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
            var compiledStatement = codeGenerator.Generate(line.Statement);

            if (line.Number.HasValue)
            {
                var sourceCode = codeFormatter.Format(line.Statement);
                rte.Statements[line.Number.Value] = new Statement(sourceCode, compiledStatement);
                return compiledStatement.ToString();
            }
            else
                return compiledStatement(rte);
        }

        public void Print(string result)
        {
            if (!string.IsNullOrEmpty(result))
                rte.InputOutput.WriteLine(result);
        }
    }
}
