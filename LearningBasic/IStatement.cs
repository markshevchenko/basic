namespace LearningBasic
{
    /// <summary>
    /// Declares a statement without line number.
    /// </summary>
    public interface IStatement
    {
        /// <summary>
        /// Runs the statement inside the specific <see cref="IRunTimeEnvironment"/>.
        /// </summary>
        /// <param name="rte">The run-time environment.</param>
        /// <returns>The string result of statement running.</returns>
        /// <remarks>The result can be printed at the Print stage of a Read-Evalueate-Print loop.</remarks>
        string Run(IRunTimeEnvironment rte);

        /// <summary>
        /// Returns the well-formatted source code of the statement.
        /// </summary>
        /// <returns>The well-formatter source code of the statement.</returns>
        string ToString();
    }
}
