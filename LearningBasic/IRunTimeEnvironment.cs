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
        /// Gets a value indicating whether the current run-time environment has been closed.
        /// </summary>
        bool IsClosed { get; }

        /// <summary>
        /// Gets a value indicating whether the current environment runs the program <see cref="Lines"/>.
        /// </summary>
        bool IsRunning { get; }

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
        /// Executes <see cref="Lines">lines</see> till the end, or infinitly.
        /// </summary>
        /// <returns>The result of program runned.</returns>
        ProgramResult Run();

        /// <summary>
        /// Terminates the running program.
        /// </summary>
        void End();

        /// <summary>
        /// Sets the next executable <see cref="ILine">line</see> of the <see cref="Run">running</see> program.
        /// </summary>
        /// <param name="number">The number of the next executable line.</param>
        void Goto(int number);

        /// <summary>
        /// Starts new pseudo-random sequence, using specified seed value.
        /// </summary>
        /// <param name="seed">A number used to calcuate a starting value for pseudo-random sequence.</param>
        void Randomize(int seed);
    }
}
