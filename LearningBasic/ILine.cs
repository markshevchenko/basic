namespace LearningBasic
{

    /// <summary>
    /// Declares a program line that contains a statement and possible line number;
    /// </summary>
    public interface ILine : IAstNode
    {
        int? Number { get; }

        IStatement Statement { get; }
    }
}
