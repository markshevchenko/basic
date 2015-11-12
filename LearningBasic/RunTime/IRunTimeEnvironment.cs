namespace LearningBasic.RunTime
{
    using System.Collections.Generic;
    using LearningBasic.IO;

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
        /// Gets a value indicating whether the current environment is running the  <see cref="Lines">program</see>.
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// Gets the last used program name.
        /// </summary>
        /// <remarks>
        /// The property is changed by <see cref="Load(string)"/> and <see cref="Save(string)"/> methods.
        /// </remarks>
        string LastUsedName { get; }

        /// <summary>
        /// Gets the <see cref="IInputOutput">input/output</see> object.
        /// </summary>
        IInputOutput InputOutput { get; }

        /// <summary>
        /// Gets the variables dictionary.
        /// </summary>
        IDictionary<string, dynamic> Variables { get; }

        /// <summary>
        /// Gets the program lines.
        /// </summary>
        IReadOnlyList<ILine> Lines { get; }

        /// <summary>
        /// Searches the entire sorted list of lines for a specified line and returns
        /// zero-based index of the line.
        /// </summary>
        /// <param name="line">The line to locate.</param>
        /// <returns>
        /// The zero-based index of a line int sorted list, if item is found; otherwise,
        /// a negative number that is the bitwise complement of the index of the next
        /// line that is larger than a line or, if there is no larger line, the bitwise
        /// complement of the count of <see cref="Lines"/>.
        /// </returns>
        int BinarySearch(ILine line);

        /// <summary>
        /// Adds the specified line to the program, or updates existing line, if it has the same label.
        /// </summary>
        /// <param name="line">The program line.</param>
        void AddOrUpdate(ILine line);

        /// <summary>
        /// Removes a range of element form the list of lines.
        /// </summary>
        /// <param name="index">The zero-based starting index of the range of lines to remove.</param>
        /// <param name="count">The number of lines to remove.</param>
        void RemoveRange(int index, int count);

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
        /// Runs the <see cref="Lines">lines</see> till the end, or infinitly.
        /// </summary>
        /// <returns>The result of program runned.</returns>
        ProgramResult Run();

        /// <summary>
        /// Ends the running program.
        /// </summary>
        void End();

        /// <summary>
        /// Sets the next executable <see cref="ILine">line</see> in the
        /// <see cref="Run">running</see> <see cref="Lines">program</see>.
        /// </summary>
        /// <param name="label">The label of the line.</param>
        void Goto(string label);

        /// <summary>
        /// Starts new pseudo-random sequence, using specified seed value.
        /// </summary>
        /// <param name="seed">A number used to calcuate a starting value for pseudo-random sequence.</param>
        void Randomize(int seed);

        /// <summary>
        /// Gets a value indicating whether running line contains non-started miltiline loop.
        /// </summary>
        bool IsNewLoop { get; }

        /// <summary>
        /// Starts the loop.
        /// </summary>
        /// <param name="loop">The loop to repeated running.</param>
        /// <returns><c>true</c> if new loop started, <c>false</c> if the loop is already started.</returns>
        void StartLoop(ILoop loop);

        /// <summary>
        /// Gets a value indicating whether the last <see cref="StartLoop(ILoop)">started loop</see> is over.
        /// </summary>
        bool IsLastLoopOver { get; }

        /// <summary>
        /// Takes a step of the last <see cref="StartLoop(ILoop)">started loop</see>.
        /// </summary>
        void TakeLastLoopStep();

        /// <summary>
        /// Repeats the last started loop.
        /// </summary>
        void RepeatLastLoop();

        /// <summary>
        /// Stops the last <see cref="StartLoop(ILoop)">started loop</see>.
        /// </summary>
        void StopLastLoop();
    }
}