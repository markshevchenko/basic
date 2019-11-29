namespace Basic.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Basic.IO;

    /// <summary>
    /// Implements the run-time environment.
    /// </summary>
    public class RunTimeEnvironment : IDisposable
    {
        private readonly IInputOutput inputOutput;
        private readonly IProgramRepository programRepository;
        private readonly Variables variables;

        /// <summary>
        /// Gets a value indicating whether the current run-time environment has been closed.
        /// </summary>
        public virtual bool IsClosed { get { return IsDisposed; } }

        /// <summary>
        /// Gets a value indicating whether the current environment is running the  <see cref="Lines">program</see>.
        /// </summary>
        public virtual bool IsRunning { get { return Runner != null; } }

        /// <summary>
        /// Gets the <see cref="IInputOutput">input/output</see> object.
        /// </summary>
        public virtual IInputOutput InputOutput { get { return inputOutput; } }

        /// <summary>
        /// Gets the variables dictionary.
        /// </summary>
        public virtual Variables Variables { get { return variables; } }

        /// <summary>
        /// Gets the program lines.
        /// </summary>
        public virtual List<Line> Lines { get; private set; }

        /// <inheritdoc />
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// The runner of program.
        /// </summary>
        public ProgramRunner Runner { get; private set; }

        /// <summary>
        /// The stack of multiline loops.
        /// </summary>
        internal Stack<MultilineLoop> StackOfLoops { get; private set; }

        /// <summary>
        /// Creates an instance of <see cref="RunTimeEnvironment"/>.
        /// </summary>
        /// <param name="inputOutput">The input/output object.</param>
        /// <param name="programRepository">The program repository.</param>
        public RunTimeEnvironment(IInputOutput inputOutput, IProgramRepository programRepository)
        {
            if (inputOutput == null)
                throw new ArgumentNullException(nameof(inputOutput));

            if (programRepository == null)
                throw new ArgumentNullException(nameof(programRepository));

            this.inputOutput = inputOutput;
            this.inputOutput.OnBreak += InputOutput_OnBreak;
            this.programRepository = programRepository;
            this.variables = new Variables();

            IsDisposed = false;
            Lines = new List<Line>();
            StackOfLoops = new Stack<MultilineLoop>();
        }

        /// <summary>
        /// Searches the entire sorted list of lines for a specified line and returns
        /// zero-based index of the line.
        /// </summary>
        /// <param name="line">The line to locate.</param>
        /// <returns>
        /// The zero-based index of a line int sorted list, if item is found; otherwise,
        /// a negative number that is the bitwise complement of the index of the next
        /// line that is larger than a line or, if there is no larger line, the bitwise
        /// complement of the count of <see cref="Lines"/>.
        /// </returns>
        public int BinarySearch(Line line)
        {
            if (line == null)
                throw new ArgumentNullException(nameof(line));

            if (string.IsNullOrEmpty(line.Label))
                throw new ArgumentException(ErrorMessages.EmptyLabel, nameof(line));

            return Lines.BinarySearch(line);
        }

        /// <summary>
        /// Adds the specified line to the program, or updates existing line, if it has the same label.
        /// </summary>
        /// <param name="line">The program line.</param>
        public void AddOrUpdate(Line line)
        {
            if (line == null)
                throw new ArgumentNullException(nameof(line));

            if (string.IsNullOrEmpty(line.Label))
                throw new ArgumentException(ErrorMessages.EmptyLabel, nameof(line));

            var index = Lines.BinarySearch(line);

            if (index < 0)
                Lines.Insert(~index, line);
            else
                Lines[index] = line;
        }


        /// <summary>
        /// Removes a range of element form the list of lines.
        /// </summary>
        /// <param name="index">The zero-based starting index of the range of lines to remove.</param>
        /// <param name="count">The number of lines to remove.</param>
        public void RemoveRange(int index, int count)
        {
            Lines.RemoveRange(index, count);
        }

        /// <summary>
        /// Closes the run-time environment.
        /// </summary>
        public virtual void Close()
        {
            ThrowIfDisposed();

            Dispose();
        }

        /// <summary>
        /// Saves the current program with specified name.
        /// </summary>
        /// <param name="name">The name to save.</param>
        public virtual void Save(string name)
        {
            ThrowIfDisposed();
            ThrowIfRunning();

            if (name == null)
                throw new ArgumentNullException(nameof(name));

            programRepository.Save(name, Lines);
            variables.LastUsedProgramName = name;
        }

        /// <summary>
        /// Loads the program with specified name.
        /// </summary>
        /// <param name="name">The name to load.</param>
        public virtual void Load(string name)
        {
            ThrowIfDisposed();
            ThrowIfRunning();

            if (name == null)
                throw new ArgumentNullException(nameof(name));

            var lines = programRepository.Load(name);
            Lines = new List<Line>(lines);
            variables.LastUsedProgramName = name;
        }

        /// <summary>
        /// Runs the <see cref="Lines">lines</see> till the end, or infinitly.
        /// </summary>
        /// <returns>The result of program runned.</returns>
        public virtual ProgramResult Run()
        {
            ThrowIfDisposed();
            ThrowIfRunning();

            StartRun();
            try
            {
                return TakeRunSteps();
            }
            finally
            {
                StopRun();
            }
        }

        protected virtual internal void StartRun()
        {
            Runner = new ProgramRunner(Lines);
        }

        protected virtual internal ProgramResult TakeRunSteps()
        {
            while (Runner.MoveNext())
                Runner.ExecuteCurrentStatement(this);

            if (Runner.IsBroke)
                return ProgramResult.Broken;

            return ProgramResult.Completed;
        }

        protected virtual internal void StopRun()
        {
            Runner = null;
        }

        /// <summary>
        /// Ends the running program.
        /// </summary>
        public virtual void End()
        {
            ThrowIfDisposed();
            ThrowIfNotRunning();

            Runner.Complete();
        }

        /// <summary>
        /// Sets the next executable <see cref="Line">line</see> in the
        /// <see cref="Run">running</see> <see cref="Lines">program</see>.
        /// </summary>
        /// <param name="label">The label of the line.</param>
        public virtual void Goto(string label)
        {
            ThrowIfDisposed();
            ThrowIfNotRunning();

            Runner.Goto(label);
        }

        /// <summary>
        /// Starts new pseudo-random sequence, using specified seed value.
        /// </summary>
        /// <param name="seed">A number used to calcuate a starting value for pseudo-random sequence.</param>
        public virtual void Randomize(int seed)
        {
            ThrowIfDisposed();

            Variables.Random = new Random(seed);
        }

        /// <summary>
        /// Gets a value indicating whether loop at current running line is started already.
        /// </summary>
        /// <returns><c>true</c> if the loop is already started; otherwise, <c>false</c>.</returns>
        public bool IsLoopStarted
        {
            get
            {
                ThrowIfDisposed();
                ThrowIfNotRunning();

                return StackOfLoops.Any(l => l.StartLine == Runner.RunningLine);
            }
        }

        /// <summary>
        /// Starts the loop.
        /// </summary>
        /// <param name="loop">The loop to repeated running.</param>
        /// <returns><c>true</c> if new loop started, <c>false</c> if the loop is already started.</returns>
        public void StartLoop(ILoop loop)
        {
            ThrowIfDisposed();
            ThrowIfNotRunning();

            if (loop == null)
                throw new ArgumentNullException(nameof(loop));

            var multilineLoop = new MultilineLoop(Runner.RunningLine, loop);
            StackOfLoops.Push(multilineLoop);
        }

        /// <summary>
        /// Gets a value indicating whether the last <see cref="StartLoop(ILoop)">started loop</see> is over.
        /// </summary>
        public bool IsLastLoopOver
        {
            get
            {
                ThrowIfDisposed();
                ThrowIfNotRunning();
                ThrowIfThereIsNotLastLoop();

                return StackOfLoops.Peek().IsOver;
            }
        }

        /// <summary>
        /// Takes a step of the last <see cref="StartLoop(ILoop)">started loop</see>.
        /// </summary>
        public void TakeLastLoopStep()
        {
            ThrowIfDisposed();
            ThrowIfNotRunning();
            ThrowIfThereIsNotLastLoop();

            StackOfLoops.Peek().TakeStep();
        }

        /// <summary>
        /// Repeats the last started loop.
        /// </summary>
        public void RepeatLastLoop()
        {
            ThrowIfDisposed();
            ThrowIfNotRunning();
            ThrowIfThereIsNotLastLoop();

            var startLabel = GetStartLabelOfLastLoop();
            Goto(startLabel);
        }

        internal string GetStartLabelOfLastLoop()
        {
            return StackOfLoops.Peek().StartLine.Label;
        }

        /// <summary>
        /// Stops the last <see cref="StartLoop(ILoop)">started loop</see>.
        /// </summary>
        public void StopLastLoop()
        {
            ThrowIfDisposed();
            ThrowIfNotRunning();
            ThrowIfThereIsNotLastLoop();

            StackOfLoops.Pop();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (IsDisposed)
                return;

            if (isDisposing)
                InputOutput.OnBreak -= InputOutput_OnBreak;

            IsDisposed = true;
        }

        internal void InputOutput_OnBreak(object sender, EventArgs e)
        {
            if (!IsRunning)
                return;

            Runner.Break();
        }

        private void ThrowIfDisposed()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().FullName);
        }

        private void ThrowIfRunning()
        {
            if (IsRunning)
                throw new InvalidOperationException(ErrorMessages.ProgramIsRunning);
        }

        private void ThrowIfNotRunning()
        {
            if (!IsRunning)
                throw new InvalidOperationException(ErrorMessages.ProgramIsNotRunning);
        }

        private void ThrowIfThereIsNotLastLoop()
        {
            if (StackOfLoops.Count == 0)
                throw new InvalidOperationException(ErrorMessages.UnexpectedEndOfLoop);
        }
    }
}
