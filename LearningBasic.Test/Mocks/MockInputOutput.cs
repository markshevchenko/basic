namespace LearningBasic.Mocks
{
    using System;
    using System.Collections.Generic;
    using LearningBasic.IO;

    public class MockInputOutput : IInputOutput
    {
        private readonly string[] inputStrings;
        private readonly List<string> outputStrings;
        private int lastInputedStringIndex;

        public event EventHandler OnBreak;

        public IReadOnlyList<string> OutputStrings { get { return outputStrings; } }

        private string LastOutputString
        {
            get { return outputStrings[outputStrings.Count - 1]; }
            set { outputStrings[outputStrings.Count - 1] = value; }
        }

        public MockInputOutput()
            : this(new string[0])
        { }

        public MockInputOutput(params string[] inputStrings)
        {
            this.inputStrings = inputStrings;
            this.lastInputedStringIndex = -1;
            this.outputStrings = new List<string> { string.Empty };
        }

        public string ReadLine()
        {
            if (lastInputedStringIndex == inputStrings.Length - 1)
                throw new InvalidOperationException();

            return inputStrings[++lastInputedStringIndex];
        }

        public void Write(string s)
        {
            LastOutputString += s;
        }

        public void Write(string format, params object[] args)
        {
            LastOutputString += string.Format(format, args);
        }

        public void WriteLine()
        {
            outputStrings.Add(string.Empty);
        }

        public void WriteLine(string s)
        {
            Write(s);
            WriteLine();
        }

        public void WriteLine(string format, params object[] args)
        {
            Write(format, args);
            WriteLine();
        }

        public void Break()
        {
            var onBreak = OnBreak;
            if (onBreak != null)
                onBreak(this, EventArgs.Empty);
        }
    }
}
