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
        /// <returns>The result of a statement (an usual string or <see cref="Result.HasValue">nothing</see>).</returns>
        Result Run(IRunTimeEnvironment rte);
    }
}
