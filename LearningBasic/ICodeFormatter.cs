namespace LearningBasic
{
    /// <summary>
    /// Declares the interface of a statement's code formatter.
    /// </summary>
    /// <typeparam name="TTag">The type of the tags of the Abstract Syntax Tree.</typeparam>
    public interface ICodeFormatter<TTag>
        where TTag : struct
    {
        /// <summary>
        /// Formats the source code from the root node of the statement's Abstract Syntax Tree.
        /// </summary>
        /// <param name="statement">The root node of the statement's Abstract Syntax Tree.</param>
        /// <returns>The well-formatted code.</returns>
        string Format(AstNode<TTag> statement);
    }
}
