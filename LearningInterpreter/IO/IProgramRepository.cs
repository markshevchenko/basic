namespace LearningInterpreter.IO
{
    using System.Collections.Generic;
    using LearningInterpreter.RunTime;

    /// <summary>
    /// Declares load and save operations.
    /// </summary>
    public interface IProgramRepository
    {
        /// <summary>
        /// Loads a program from an external storage with the specified <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name of the program, f.e. a file name.</param>
        /// <returns>
        /// The program as a <see cref="IReadOnlyList{T}>list</see>
        /// of <see cref="IAstNode">lines</see>.
        /// </returns>
        IReadOnlyList<IAstNode> Load(string name);

        /// <summary>
        /// Saves a program to an external storage with the specified <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name of the program, f.e. a file name.</param>
        /// <param name="lines">
        /// The program as a <see cref="IReadOnlyList{T}>list</see>
        /// of <see cref="IAstNode">lines</see>.
        /// </param>
        void Save(string name, IReadOnlyList<IAstNode> lines);
    }
}
