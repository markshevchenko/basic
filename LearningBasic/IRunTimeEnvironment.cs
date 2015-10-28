namespace LearningBasic
{
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Declares the interface of the run-time environment.
    /// </summary>
    public interface IRunTimeEnvironment
    {
        /// <summary>
        /// Gets the <see cref="IInputOutput"/> object.
        /// </summary>
        IInputOutput InputOutput { get; }

        /// <summary>
        /// Retrieves a Boolean value that indicates whether the specified environment instance has been terminated.
        /// </summary>
        bool IsTerminated { get; }

        /// <summary>
        /// Gets the variables dictionary.
        /// </summary>
        IDictionary<string, dynamic> Variables { get; }

        /// <summary>
        /// Adds the command with specified line number.
        /// </summary>
        /// <param name="lineNumber">The line number.</param>
        /// <param name="command">The command.</param>
        void AddCommand(int lineNumber, Command command);

        /// <summary>
        /// Terminates the work session of the run-time environment.
        /// </summary>
        void Terminate();
    }
}
