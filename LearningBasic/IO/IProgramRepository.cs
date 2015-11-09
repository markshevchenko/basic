namespace LearningBasic.IO
{
    using System.Collections.Generic;
    using LearningBasic.RunTime;

    /// <summary>
    /// Declares load and save operations.
    /// </summary>
    public interface IProgramRepository
    {
        /// <summary>
        /// Loads the BASIC program from an external storage with the specified <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name of the program, f.e. a file name.</param>
        /// <returns>
        /// The program as a <see cref="IReadOnlyList{T}>list</see>
        /// of <see cref="ILine">lines</see>.
        /// </returns>
        IReadOnlyList<ILine> Load(string name);

        /// <summary>
        /// Saves the program to an external storage with the specified <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name of the program, f.e. a file name.</param>
        /// <param name="lines">
        /// The program as a <see cref="IReadOnlyList{T}>list</see>
        /// of <see cref="ILine">lines</see>.
        /// </param>
        void Save(string name, IReadOnlyList<ILine> lines);
    }
}
