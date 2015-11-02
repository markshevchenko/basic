namespace LearningBasic
{
    using System.Collections.Generic;

    /// <summary>
    /// Declares the interface of the run-time environment.
    /// </summary>
    public interface IRunTimeEnvironment
    {
        /// <summary>
        /// Gets the <see cref="IInputOutput"/> object.
        /// </summary>
        /// <remarks>This object incapsulates all input-output operations.</remarks>
        IInputOutput InputOutput { get; }

        /// <summary>
        /// Get the <see cref="IProgramRepository"/>.
        /// </summary>
        /// <remarks>This repository incapsulates save and load operations.</remarks>
        IProgramRepository ProgramRepository { get; }

        /// <summary>
        /// Retrieves a <c>Boolean</c> value that indicates whether the specified environment instance has been closed.
        /// </summary>
        bool IsClosed { get; }

        /// <summary>
        /// Gets the variables dictionary.
        /// </summary>
        IDictionary<string, dynamic> Variables { get; }

        /// <summary>
        /// Gets the sorted lines' list.
        /// </summary>
        SortedList<int, IStatement> Lines { get; }

        /// <summary>
        /// Closes the run-time environment.
        /// </summary>
        void Close();
    }
}
