namespace Basic
{
    /// <summary>
    /// Declares the interface of the syntax analyzer (parser) of a line-based language.
    /// </summary>
    /// <typeparam name="TTag">The type of the tags of abstract syntax tree nodes.</typeparam>
    public interface IParser<TTag>
        where TTag : struct
    {
        /// <summary>
        /// Parses a single line and returns an Abstract Syntax Tree node.
        /// </summary>
        /// <param name="line">The program line.</param>
        /// <returns>The Abstract Syntax Tree node.</returns>
        AstNode<TTag> Parse(string line);
    }
}
