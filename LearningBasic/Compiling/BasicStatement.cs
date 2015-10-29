namespace LearningBasic.Compiling
{
    using System;

    /// <summary>
    /// Represents a BASIC's statement.
    /// </summary>
    public class BasicStatement: IStatement
    {
        private readonly AstNode<Tag> statementRoot;
        private readonly Func<IRunTimeEnvironment, string> function;

        /// <summary>
        /// Initializes a new instance of <see cref="BasicStatement"/>.
        /// </summary>
        /// <param name="statementRoot">The root node of the statement's Abstract Syntax Tree.</param>
        /// <param name="function">The compiled statement.</param>
        public BasicStatement(AstNode<Tag> statementRoot, Func<IRunTimeEnvironment, string> function)
        {
            if (statementRoot == null)
                throw new ArgumentNullException("sourceCode");

            if (function == null)
                throw new ArgumentNullException("function");

            this.statementRoot = statementRoot;
            this.function = function;
        }

        /// <summary>
        /// Runs the statement inside the specified <see cref="IRunTimeEnvironment"/>.
        /// </summary>
        /// <param name="rte">The run-time environment.</param>
        /// <returns>The result of a statement in printable form.</returns>
        public string Run(IRunTimeEnvironment rte)
        {
            return function(rte);
        }

        /// <summary>
        /// Returns well-formed BASIC source code of the statement.
        /// </summary>
        /// <returns>The well-formed BASIC source code.</returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
