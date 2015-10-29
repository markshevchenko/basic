namespace LearningBasic
{
    using System;

    /// <summary>
    /// Represents a statement that can be runned and printed.
    /// </summary>
    public class Statement
    {
        private readonly string sourceCode;
        private readonly Func<IRunTimeEnvironment, string> compiledStatement;

        /// <summary>
        /// Initializes a new instance of <see cref="Statement"/>.
        /// </summary>
        /// <param name="sourceCode">The statement's source code.</param>
        /// <param name="compiledStatement">The compiled statement.</param>
        public Statement(string sourceCode, Func<IRunTimeEnvironment, string> compiledStatement)
        {
            if (sourceCode == null)
                throw new ArgumentNullException("sourceCode");

            if (compiledStatement == null)
                throw new ArgumentNullException("compiledStatement");

            this.sourceCode = sourceCode;
            this.compiledStatement = compiledStatement;
        }

        /// <summary>
        /// Runs the statement inside the specified <see cref="IRunTimeEnvironment"/>.
        /// </summary>
        /// <param name="rte">The run-time environment.</param>
        /// <returns>The result of a statement in printable form.</returns>
        public string Run(IRunTimeEnvironment rte)
        {
            return compiledStatement(rte);
        }

        /// <summary>
        /// Returns well-formed BASIC source code of the statement.
        /// </summary>
        /// <returns>The well-formed BASIC source code.</returns>
        public override string ToString()
        {
            return sourceCode;
        }
    }
}
