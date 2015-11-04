namespace LearningBasic
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Declares the interface of the run-time environment.
    /// </summary>
    public interface IRunTimeEnvironment
    {
        /// <summary>
        /// Gets the last used program name.
        /// </summary>
        /// <remarks>
        /// The <see cref="Load(string)"/> and <see cref="Save(string)"/> methods
        /// sets <see cref="LastUsedName"/>.
        /// </remarks>
        string LastUsedName { get; }

        /// <summary>
        /// Gets the <see cref="IInputOutput"/> object.
        /// </summary>
        /// <remarks>This object incapsulates all input-output operations.</remarks>
        IInputOutput InputOutput { get; }

        /// <summary>
        /// Gets a value indicating whether the current run-time environment has been closed.
        /// </summary>
        bool IsClosed { get; }

        /// <summary>
        /// Gets a value indicating whether the current environment runs the program <see cref="Lines"/>.
        /// </summary>
        bool IsRunning { get; }

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

        /// <summary>
        /// Saves the current program with last used name.
        /// </summary>
        void Save();

        /// <summary>
        /// Saves the current program with specified name.
        /// </summary>
        /// <param name="name">The name to save.</param>
        void Save(string name);

        /// <summary>
        /// Loads the program with specified name.
        /// </summary>
        /// <param name="name">The name to load.</param>
        void Load(string name);

        /// <summary>
        /// Runs the program stored in <see cref="Lines">.
        /// </summary>
        /// <returns>The result of running.</returns>
        EvaluateResult Run();

        /// <summary>
        /// Sets the next executable <see cref="ILine">line</see> of the <see cref="Run">running</see> program.
        /// </summary>
        /// <param name="number">The number of the next executable line.</param>
        void Goto(int number);
    }
}
