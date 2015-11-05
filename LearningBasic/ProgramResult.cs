namespace LearningBasic
{
    using System;

    /// <summary>
    /// Implemets possible program results.
    /// </summary>
    public class ProgramResult
    {
        /// <summary>
        /// The program is broke by the user.
        /// </summary>
        public bool IsBroken { get; private set; }

        /// <summary>
        /// The program is completed in natural way.
        /// </summary>
        public bool IsCompleted { get; private set; }

        /// <summary>
        /// The program is aborted due an uncatched exception.
        /// </summary>
        public bool IsAborted { get; private set; }

        /// <summary>
        /// The exception occured if the program <see cref="IsAborted">is aborted</see>; otherwise, <c>null</c>.
        /// </summary>
        public Exception Exception { get; private set; }

        private ProgramResult()
        { }

        /// <summary>
        /// Creates the result of a broken program.
        /// </summary>
        /// <returns>The instance with <see cref="IsBroken"/> property set.</returns>
        public static ProgramResult CreateBroken()
        {
            return new ProgramResult { IsBroken = true };
        }

        /// <summary>
        /// Creates the result of a completed program.
        /// </summary>
        /// <returns>The instance with <see cref="IsCompleted"/> property set.</returns>
        public static ProgramResult CreateCompleted()
        {
            return new ProgramResult { IsCompleted = true };
        }

        /// <summary>
        /// Creates the result of an aborted program.
        /// </summary>
        /// <param name="exception">The exception that is a reason of abort. Can be <c>null</c>.</param>
        /// <returns>The instance with <see cref="IsAborted"/> property set.</returns>
        public static ProgramResult CreateAborted(Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException("exception");

            return new ProgramResult { IsAborted = true, Exception = exception };
        }
    }
}
