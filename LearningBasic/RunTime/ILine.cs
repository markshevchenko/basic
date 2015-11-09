namespace LearningBasic.RunTime
{

    /// <summary>
    /// Declares a program line that contains a statement and a label.
    /// </summary>
    public interface ILine
    {
        /// <summary>
        /// Gets the label of the program line.
        /// </summary>
        string Label { get; }

        /// <summary>
        /// Gets the statement of the program line.
        /// </summary>
        IStatement Statement { get; }
    }
}
