namespace LearningBasic.Evaluating
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

        private readonly SortedList<int, IStatement> lines;
        private volatile bool isBroke;
        private int currentLineIndex;
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
        /// <param name="lines">The line dictionary to run.</param>
        /// <remarks>
        /// The <see cref="KeyValuePair{TKey, TValue}.Key">key</see> of dictionary is
        /// an integer line number, and the <see cref="KeyValuePair{TKey, TValue}.Value">value</see>
        /// is s <see cref="IStatement">statement</see>.
        /// </remarks>
        public ProgramRunner(IDictionary<int, IStatement> lines)
        {
            if (lines == null)
                throw new ArgumentNullException("lines");

            this.lines = new SortedList<int, IStatement>(lines);
            this.currentLineIndex = BeforeFirstLine;
            this.isBroke = false;
            this.executionOrder = ExecutionOrder.LineByLine;
        }

        /// <summary>
        /// The current statement to <see cref="IStatement.Execute(IRunTimeEnvironment)">evalutate</see>.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">The <see cref="ProgramRunner"/> is not in running state.</exception>
        public IStatement CurrentStatement { get { return lines[lines.Keys[currentLineIndex]]; } }

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
                ++currentLineIndex;
                ThrowIfMoveNextRecalledAfterFalse();
            }
            else
                executionOrder = ExecutionOrder.LineByLine;

            return currentLineIndex < lines.Count;
        }

        private void ThrowIfMoveNextRecalledAfterFalse()
        {
            if (currentLineIndex > lines.Count)
                throw new InvalidOperationException(ErrorMessages.ProgramIsNotRunning);
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
        /// Advances the <see cref="ProgramRunner"/> on the line with the specified number.
        /// </summary>
        /// <param name="number">The line number to go.</param>
        /// <exception cref="InvalidOperationException">The line with the specified number is not exists.</exception>
        public void Goto(int number)
        {
            var lineIndex = lines.IndexOfKey(number);

            if (lineIndex == -1)
            {
                var message = string.Format(ErrorMessages.LineNumberNotFound, number);
                throw new InvalidOperationException(message);
            }

            currentLineIndex = lineIndex;
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
            currentLineIndex = lines.Count;
            executionOrder = ExecutionOrder.ArbitraryLine;
        }
    }
}
