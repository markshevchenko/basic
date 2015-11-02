namespace LearningBasic
{
    /// <summary>
    /// Declares a syntax analyzer (parser) of a line-based language.
    /// </summary>
    public interface ILineParser
    {
        /// <summary>
        /// Parses a single line and returns it in <see cref="ILine">parsed form</see>.
        /// </summary>
        /// <param name="line">The single program line.</param>
        /// <returns>The parsed line.</returns>
        ILine Parse(string line);
    }
}
