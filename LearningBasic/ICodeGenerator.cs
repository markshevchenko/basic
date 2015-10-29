namespace LearningBasic
{
    using System;

    /// <summary>
    /// Declares the interface of a statement's code generator.
    /// </summary>
    /// <typeparam name="TTag">The type of the tags of the Abstract Syntax Tree.</typeparam>
    public interface ICodeGenerator<TTag>
        where TTag : struct
    {
        /// <summary>
        /// Generates the compiled statement to run inside
        /// <see cref="IRunTimeEnvironment">run-time environment</see>.
        /// </summary>
        /// <param name="statement">The root node of the statement's Abstract Syntax Tree.</param>
        /// <returns>The compiled statement.</returns>
        Func<IRunTimeEnvironment, string> Generate(AstNode<TTag> statement);
    }
}
