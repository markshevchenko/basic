namespace LearningBasic.RunTime
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Implements automatic BASIC program runner.
    /// </summary>
    public sealed class ProgramRunner
    {
        private enum ExecutionOrder
        {
            LineByLine,

            ArbitraryLine,
        }

        private const int BeforeFirstLine = -1;

        private readonly List<ILine> lines;
        private volatile bool isBroke;
        private int runningLineIndex;
        private ExecutionOrder executionOrder;

        /// <summary>
        /// Gets a value indicating whether the runner is broke by <see cref="Break"/> method.
        /// </summary>
        public bool IsBroke
        {
            // Use manual (not automatic) property cause isBroke is volatile.
            get { return isBroke; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramRunner"/> class.
        /// </summary>
        /// <param name="lines">The program lines to run.</param>
        public ProgramRunner(IEnumerable<ILine> lines)
        {
            if (lines == null)
                throw new ArgumentNullException("lines");

            this.lines = new List<ILine>(lines);
            this.runningLineIndex = BeforeFirstLine;
            this.isBroke = false;
            this.executionOrder = ExecutionOrder.LineByLine;
        }

        /// <summary>
        /// The running line.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">The <see cref="ProgramRunner"/> is not in running state.</exception>
        public ILine RunningLine { get { return lines[runningLineIndex]; } }

        /// <summary>
        /// Advances the <see cref="ProgramRunner"/> on the next line in order.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the runner was successfully advanced on the next line;
        /// <c>false</c> if the runner has passed the end of the lines, or <see cref="IsBroke">is breaked</see>.
        /// </returns>
        /// <exception cref="InvalidOperationException">The method was called after it returns <c>false</c>.</exception>
        public bool MoveNext()
        {
            if (IsBroke)
                return false;

            if (executionOrder == ExecutionOrder.LineByLine)
            {
                ++runningLineIndex;
                ThrowIfMoveNextRecalledAfterFalse();
            }
            else
            {
                // Do nothing in ExecutionOrder.ArbitraryLine state, just return to ExecutionOrder.LineByLine.
                executionOrder = ExecutionOrder.LineByLine;
            }

            return runningLineIndex < lines.Count;
        }

        private void ThrowIfMoveNextRecalledAfterFalse()
        {
            if (runningLineIndex > lines.Count)
                throw new InvalidOperationException(ErrorMessages.NoMoreLines);
        }

        /// <summary>
        /// Breaks the <see cref="ProgramRunner"/>.
        /// </summary>
        /// <remarks>
        /// Unline the <see cref="Complete"/> method is used to break the program by the user.
        /// F.e. this method can be used to implement Ctrl+Break handler.
        /// </remarks>
        public void Break()
        {
            isBroke = true;
        }

        /// <summary>
        /// Advances the <see cref="ProgramRunner"/> to the line with the specified label.
        /// </summary>
        /// <param name="labelr">The label of the line to go.</param>
        /// <exception cref="ArgumentOutOfRangeException">The line with the specified label is not exists.</exception>
        public void Goto(string label)
        {
            var lineIndex = lines.FindIndex(line => line.Label == label);

            if (lineIndex == -1)
            {
                var message = string.Format(ErrorMessages.LabelNotFound, label);
                throw new ArgumentOutOfRangeException(message);
            }

            runningLineIndex = lineIndex;
            executionOrder = ExecutionOrder.ArbitraryLine;
        }

        /// <summary>
        /// Completes the <see cref="ProgramRunner"/> advancing it after the last line of the program.
        /// </summary>
        /// <remarks>
        /// Unlike the <see cref="Break"/> method is used to complete the program in natural way.
        /// F.e. this method can be used to implement the END statement.
        /// </remarks>
        public void Complete()
        {
            runningLineIndex = lines.Count;
            executionOrder = ExecutionOrder.ArbitraryLine;
        }

        /// <summary>
        /// Executes the statement of the <see cref="RunningLine"/>.
        /// </summary>
        /// <param name="rte">The run-time environment.</param>
        /// <returns>The result of the statement.</returns>
        public EvaluateResult ExecuteCurrentStatement(IRunTimeEnvironment rte)
        {
            return RunningLine.Statement.Execute(rte);
        }
    }
}
