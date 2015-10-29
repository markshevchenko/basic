namespace LearningBasic
{
    using System;

    /// <summary>
    /// Declares the interface of the statement compiler.
    /// </summary>
    /// <typeparam name="TTag">The type of the tags of the Abstract Syntax Tree nodes.</typeparam>
    public interface IStatementCompiler<TTag>
        where TTag : struct
    {
        /// <summary>
        /// Generates the action to run inside <see cref="IRunTimeEnvironment"/>.
        /// </summary>
        /// <param name="statement">The root node of the statement's Abstract Syntax Tree.</param>
        /// <returns>The generated action.</returns>
        Action<IRunTimeEnvironment> Compile(AstNode<TTag> statement);
    }
}
