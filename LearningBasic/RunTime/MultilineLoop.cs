namespace LearningBasic.RunTime
{
    using System;

    /// <summary>
    /// Implements <see cref="ILoop"/> decorator that has <see cref="StartLine"/> of the loop.
    /// </summary>
    public class MultilineLoop : ILoop
    {
        private readonly ILoop decoratedLoop;

        /// <summary>
        /// Gets a start line of the multiline loop.
        /// </summary>
        public ILine StartLine { get; private set; }

        /// <inheritdoc />
        public bool IsOver {  get { return decoratedLoop.IsOver; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultilineLoop"/> with the start line number and the decorated loop.
        /// </summary>
        /// <param name="startLine">The start line of the multiline loop.</param>
        /// <param name="loop">The decorated loop.</param>
        public MultilineLoop(ILine startLine, ILoop loop)
        {
            if (startLine == null)
                throw new ArgumentNullException("startLine");

            if (loop == null)
                throw new ArgumentNullException("loop");

            StartLine = startLine;
            decoratedLoop = loop;
        }

        /// <inheritdoc />
        public void TakeStep()
        {
            decoratedLoop.TakeStep();
        }
    }

}
