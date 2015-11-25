namespace LearningInterpreter.RunTime
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using LearningInterpreter.Parsing;

    /// <summary>
    /// Implements the sorted list of the program lines.
    /// </summary>
    public class Program : IReadOnlyList<IAstNode>
    {
        private readonly IComparer<string> labelComparer;
        private readonly List<string> labels;
        private readonly List<ILine> lines;

        public ILine this[string label]
        {
            get
            {
                var index = labels.BinarySearch(label);

                if (index >= 0)
                    return lines[index];

                throw new KeyNotFoundException();
            }
        }

        public ILine this[int index]
        {
            get { return lines[index]; }
        }

        public int Count
        {
            get { return lines.Count; }
        }

        public Program(IComparer<string> labelComparer)
        {
            if (labelComparer == null)
                throw new ArgumentNullException("labelComparer");

            this.labelComparer = labelComparer;
            this.labels = new List<string>();
            this.lines = new List<ILine>();
        }

        public void AddOrUpdate(ILine line)
        {
            if (line == null)
                throw new ArgumentNullException("line");

            if (string.IsNullOrEmpty(line.Label))
                throw new ArgumentException(ErrorMessages.EmptyLabel, "line");

            var index = labels.BinarySearch(line.Label);

            if (index < 0)
            {
                index = ~index;
                labels.Insert(index, line.Label);
                lines.Insert(index, line);
            }
            else
                lines[index] = line;
        }

        public IEnumerator<ILine> GetEnumerator()
        {
            return lines.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return lines.GetEnumerator();
        }
    }
}
