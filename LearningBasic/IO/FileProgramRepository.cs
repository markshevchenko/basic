namespace LearningBasic.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Implements the file program repository to load/save a program from/to a file.
    /// </summary>
    public class FileProgramRepository : IProgramRepository
    {
        private readonly ILineParser parser;

        /// <summary>
        /// Creates an instance of <see cref="FileProgramRepository"/>.
        /// </summary>
        /// <param name="parser">The BASIC line-based parser to load source code.</param>
        public FileProgramRepository(ILineParser parser)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");

            this.parser = parser;
        }

        /// <inheritdoc />
        public virtual IDictionary<int, IStatement> Load(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(ErrorMessages.MissingFileName, name);

            var nameWithExtention = AddExtensionIfNeeded(name);
            var lines = ReadLines(nameWithExtention);
            return Parse(parser, lines);
        }

        /// <inheritdoc />
        public virtual void Save(string name, IDictionary<int, IStatement> lines)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(ErrorMessages.MissingFileName, name);

            var nameWithExtention = AddExtensionIfNeeded(name);
            var printableLines = lines.ToPrintable();
            Save(nameWithExtention, printableLines);
        }

        public static string AddExtensionIfNeeded(string name)
        {
            var extension = Path.GetExtension(name);

            if (extension == string.Empty)
                return Path.ChangeExtension(name, ".bas");

            return name;
        }

        public static string[] ReadLines(string fileName)
        {
            try
            {
                return File.ReadAllLines(fileName);
            }
            catch (Exception exception)
            {
                var message = string.Format(ErrorMessages.CantLoadFile, fileName);
                throw new IOException(message, exception);
            }
        }

        public static IDictionary<int, IStatement> Parse(ILineParser parser, IEnumerable<string> lines)
        {
            return lines.Select(parser.Parse)
                        .Where(line => line.Number.HasValue)
                        .ToDictionary(line => line.Number.Value, line => line.Statement);
        }

        public static void Save(string fileName, IEnumerable<string> printableLines)
        {
            try
            {
                File.WriteAllLines(fileName, printableLines);
            }
            catch (Exception exception)
            {
                var message = string.Format(ErrorMessages.CantSaveFile, fileName);
                throw new IOException(message, exception);
            }
        }
    }
}
