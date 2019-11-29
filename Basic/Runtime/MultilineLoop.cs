namespace Basic.Runtime
{
    using System;

    /// <summary>
    /// Implements <see cref="ILoop"/> decorator that has <see cref="StartLine"/> of the loop.
    /// </summary>
    public class MultilineLoop : ILoop
    {
        internal ILoop DecoratedLoop { get; private set; }

        /// <summary>
        /// Gets a start line of the multiline loop.
        /// </summary>
        public Line StartLine { get; private set; }

        /// <inheritdoc />
        public bool IsOver {  get { return DecoratedLoop.IsOver; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultilineLoop"/> with the start line number and the decorated loop.
        /// </summary>
        /// <param name="startLine">The start line of the multiline loop.</param>
        /// <param name="loop">The decorated loop.</param>
        public MultilineLoop(Line startLine, ILoop loop)
        {
            if (startLine == null)
                throw new ArgumentNullException(nameof(startLine));

            if (loop == null)
                throw new ArgumentNullException(nameof(loop));

            StartLine = startLine;
            DecoratedLoop = loop;
        }

        /// <inheritdoc />
        public void TakeStep()
        {
            DecoratedLoop.TakeStep();
        }
    }

}
