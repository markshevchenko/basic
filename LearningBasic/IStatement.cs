namespace LearningBasic
{
    /// <summary>
    /// Declares a statement that can be runned within a <see cref="IRunTimeEnvironment">run-time environment</see>.
    /// </summary>
    public interface IStatement : IAstNode
    {
        /// <summary>
        /// Evaluates the statement inside the specified <see cref="IRunTimeEnvironment"/>.
        /// </summary>
        /// <param name="rte">The run-time environment.</param>
        /// <returns>The result of a statement (a message or a <see cref="EvaluateResult.Empty">nothing</see>).</returns>
        EvaluateResult Evaluate(IRunTimeEnvironment rte);
    }
}
