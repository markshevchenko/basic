namespace Basic.Runtime
{
    /// <summary>
    /// Declares a statement that can be runned within a <see cref="RunTimeEnvironment">run-time environment</see>.
    /// </summary>
    public interface IStatement
    {
        /// <summary>
        /// Executes the statement in the context of the specified <see cref="RunTimeEnvironment"/>.
        /// </summary>
        /// <param name="rte">The run-time environment.</param>
        /// <returns>The result of execution (a message or <see cref="EvaluateResult.None">nothing</see>).</returns>
        EvaluateResult Execute(RunTimeEnvironment rte);
    }
}
