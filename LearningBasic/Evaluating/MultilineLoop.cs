namespace LearningBasic.Evaluating
{
    using System;

    /// <summary>
    /// Implements <see cref="ILoop"/> decorator that has <see cref="StartLineNumber"/> of the loop.
    /// </summary>
    public class MultilineLoop : ILoop
    {
        private readonly ILoop decoratedLoop;

        /// <summary>
        /// Gets a start line number of the multiline loop.
        /// </summary>
        public int StartLineNumber { get; private set; }

        /// <inheritdoc />
        public bool IsOver {  get { return decoratedLoop.IsOver; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultilineLoop"/> with the start line number and the decorated loop.
        /// </summary>
        /// <param name="startLineNumber">The start line number of the multiline loop.</param>
        /// <param name="loop">The decorated loop.</param>
        public MultilineLoop(int startLineNumber, ILoop loop)
        {
            if (loop == null)
                throw new ArgumentNullException("loop");

            StartLineNumber = startLineNumber;
            decoratedLoop = loop;
        }

        /// <inheritdoc />
        public void TakeStep()
        {
            decoratedLoop.TakeStep();
        }
    }

}
