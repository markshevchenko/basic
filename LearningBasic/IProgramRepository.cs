using System.Collections.Generic;

namespace LearningBasic
{
    /// <summary>
    /// Implements load and save operations for a BASIC program.
    /// </summary>
    public interface IProgramRepository
    {
        /// <summary>
        /// Loads the program from an external storage with the specified <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name of storage, f.e. a file name.</param>
        /// <returns>The parsed program as a <see cref="IDictionary{TKey, TValue}">dictionary</see>
        /// of line numbers and <see cref="IStatement">statements</see>.</returns>
        IDictionary<int, IStatement> Load(string name);

        /// <summary>
        /// Saves the program to an external storage with the specified <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name of the program.</param>
        /// <param name="lines">The dictionary of line numbers and statements.</param>
        void Save(string name, IDictionary<int, IStatement> lines);
    }
}
