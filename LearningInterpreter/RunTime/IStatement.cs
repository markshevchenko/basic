namespace LearningInterpreter.RunTime
{
    using LearningInterpreter.Parsing;

    /// <summary>
    /// Declares a statement that can be runned within a <see cref="IRunTimeEnvironment">run-time environment</see>.
    /// </summary>
    public interface IStatement : IAstNode
    {
        /// <summary>
        /// Executes the statement in the context of the specified <see cref="IRunTimeEnvironment"/>.
        /// </summary>
        /// <param name="rte">The run-time environment.</param>
        /// <returns>The result of execution (a message or <see cref="EvaluateResult.None">nothing</see>).</returns>
        EvaluateResult Execute(IRunTimeEnvironment rte);
    }
}
