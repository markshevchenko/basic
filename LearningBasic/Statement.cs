namespace LearningBasic
{
    using System;

    /// <summary>
    /// Represents a BASIC statement without line number.
    /// </summary>
    /// <remarks>
    /// A statement stores itself source code and can be runned inside <see cref="IRunTimeEnvironment"/>.
    /// </remarks>
    public class Statement
    {
        private readonly AstNode<Tag> source;
        private readonly Action<IRunTimeEnvironment> compiledAction;

        /// <summary>
        /// Initializes a new instance of <see cref="Statement"/>.
        /// </summary>
        /// <param name="source">The root node of the statement's Abstract Syntax Tree.</param>
        /// <param name="compiledAction">The compiled statement's action.</param>
        public Statement(AstNode<Tag> source, Action<IRunTimeEnvironment> compiledAction)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (compiledAction == null)
                throw new ArgumentNullException("compiledAction");

            this.source = source;
            this.compiledAction = compiledAction;
        }

        /// <summary>
        /// Runs the statement inside the specified <see cref="IRunTimeEnvironment"/>.
        /// </summary>
        /// <param name="rte">The run-time environment.</param>
        public void Run(IRunTimeEnvironment rte)
        {
            compiledAction(rte);
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
