namespace LearningBasic
{
    /// <summary>
    /// Declares a statement that can be runned within a <see cref="IRunTimeEnvironment">run-time environment</see>.
    /// </summary>
    public interface IStatement : IAstNode
    {
        /// <summary>
        /// Executes the statement in the context of the specified <see cref="IRunTimeEnvironment"/>.
        /// </summary>
        /// <param name="rte">The run-time environment.</param>
        /// <returns>The result of execution (a message or <see cref="EvaluateResult.Empty">nothing</see>).</returns>
        EvaluateResult Execute(IRunTimeEnvironment rte);
    }
}
