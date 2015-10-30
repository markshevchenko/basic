namespace LearningBasic
{
    /// <summary>
    /// Declares a statement that can be runned within a <see cref="IRunTimeEnvironment">run-time environment</see>.
    /// </summary>
    public interface IStatement : IAstNode
    {
        /// <summary>
        /// Runs the statement inside the specified <see cref="IRunTimeEnvironment"/>.
        /// </summary>
        /// <param name="rte">The run-time environment.</param>
        /// <returns>The result of a statement (a message or an <see cref="StatementResult.Empty">empty</see>).</returns>
        StatementResult Run(IRunTimeEnvironment rte);
    }
}
