namespace LearningInterpreter.RunTime
{
    using System;
    using LearningInterpreter.Parsing;

    /// <summary>
    /// Declares a compiler that gets a node of Abstract Syntax Tree and compiles it to .NET method.
    /// </summary>
    public interface ICompiler
    {
        Func<IRunTimeEnvironment, EvaluateResult> Compile(IAstNode node);
    }
}
