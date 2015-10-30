namespace LearningBasic.Parsing.Ast
{
    using System.Linq.Expressions;

    /// <summary>
    /// Declares a generic expression that can be compiled to <see cref="Expression"/>.
    /// </summary>
    public interface IExpression : IAstNode
    {
        /// <summary>
        /// Compiles abstract syntax expression to <see cref="Expression">.NET expression object</see>.
        /// </summary>
        /// <param name="rte">The run-time environment to get varaibles' values.</param>
        /// <returns>.NET expression object.</returns>
        Expression Compile(IRunTimeEnvironment rte);
    }
}
