namespace LearningBasic
{
    /// <summary>
    /// Declares a loop to use with the <see cref="IRunTimeEnvironment.StartMultilineLoop"/>.
    /// </summary>
    public interface ILoop
    {
        /// <summary>
        /// Retrieves a <c>Boolean</c> value that indicates whether the specified loop has been terminated.
        /// </summary>
        bool IsOver { get; }

        /// <summary>
        /// Takes next step of the loop.
        /// </summary>
        /// <remarks>Eventually resets the <see cref="IsOver"/>.</remarks>
        void TakeStep();
    }
}
