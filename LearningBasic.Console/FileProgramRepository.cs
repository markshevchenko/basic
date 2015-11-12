namespace LearningBasic.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using LearningBasic.IO;
    using LearningBasic.Parsing;
    using LearningBasic.RunTime;

    /// <summary>
    /// Implements the file program repository to load/save a program from/to a file.
    /// </summary>
    public class FileProgramRepository : IProgramRepository
    {
        private readonly ILineParser parser;

        /// <summary>
        /// Initializes an instance of <see cref="FileProgramRepository"/> class with specified parser.
        /// </summary>
        /// <param name="parser">The line-based parser.</param>
        public FileProgramRepository(ILineParser parser)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");

            this.parser = parser;
        }

        /// <inheritdoc />
        public IReadOnlyList<ILine> Load(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(ErrorMessages.MissingFileName, name);

            var nameWithExtention = AddExtensionIfNeeded(name);
            var lines = ReadListing(nameWithExtention);
            return Parse(parser, lines);
        }

        /// <inheritdoc />
        public void Save(string name, IReadOnlyList<ILine> lines)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(ErrorMessages.MissingFileName, name);

            if (lines == null)
                throw new ArgumentNullException("lines");

            if (lines.Count == 0)
                throw new IOException(ErrorMessages.CantSaveEmptyProgram);

            var nameWithExtention = AddExtensionIfNeeded(name);
            var listing = lines.Select(line => line.ToString());
            WriteListing(nameWithExtention, listing);
        }

        public static string AddExtensionIfNeeded(string name)
        {
            var extension = System.IO.Path.GetExtension(name);

            if (extension == string.Empty)
                return System.IO.Path.ChangeExtension(name, ".bas");

            return name;
        }

        public static IReadOnlyList<ILine> Parse(ILineParser parser, IEnumerable<string> lines)
        {
            return lines.Select(parser.Parse)
                        .ToArray();
        }

        public static string[] ReadListing(string fileName)
        {
            try
            {
                return System.IO.File.ReadAllLines(fileName);
            }
            catch (Exception exception)
            {
                var message = string.Format(ErrorMessages.CantLoadFile, fileName);
                throw new IOException(message, exception);
            }
        }

        public static void WriteListing(string fileName, IEnumerable<string> listing)
        {
            try
            {
                System.IO.File.WriteAllLines(fileName, listing);
            }
            catch (Exception exception)
            {
                var message = string.Format(ErrorMessages.CantSaveFile, fileName);
                throw new IOException(message, exception);
            }
        }
    }
}
