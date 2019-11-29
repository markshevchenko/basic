namespace Basic.Runtime
{
    /// <summary>
    /// Declares a loop interface.
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
