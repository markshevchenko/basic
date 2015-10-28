namespace Basic
{
    using System;

    /// <summary>
    /// Declares the interface of the <see cref="Command">command</see> builder (code generator).
    /// </summary>
    /// <typeparam name="TTag">The type of the tags of the Abstract Syntax Tree nodes.</typeparam>
    public interface ICommandBuilder<TTag>
        where TTag : struct
    {
        /// <summary>
        /// Generates the command to run in a <see cref="IRunTimeEnvironment">runtime system</see>.
        /// </summary>
        /// <param name="statement">The root node of the Abstract Syntax Tree represents the statement.</param>
        /// <returns>The generated command.</returns>
        Command Build(AstNode<TTag> statement);
    }
}
