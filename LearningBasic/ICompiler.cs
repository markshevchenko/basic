namespace LearningBasic
{
    /// <summary>
    /// Declares the interface of the statement compiler.
    /// </summary>
    /// <typeparam name="TTag">The type of the tags of the Abstract Syntax Tree nodes.</typeparam>
    public interface ICompiler<TTag>
        where TTag : struct
    {
        /// <summary>
        /// Generates the statement to run inside <see cref="IRunTimeEnvironment"/>.
        /// </summary>
        /// <param name="statement">The root node of the statement's Abstract Syntax Tree.</param>
        /// <returns>The compiled statement.</returns>
        IStatement Compile(AstNode<TTag> statement);
    }
}
