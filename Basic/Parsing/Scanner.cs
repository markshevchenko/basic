namespace Basic.Parsing
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Implements BASIC-specific scanner.
    /// </summary>
    public class Scanner
    {
        /// <summary>
        /// Enumerates reading states.
        /// </summary>
        protected enum ScannerState
        {
            Token,

            Comment,
        };

        private readonly TextReader reader;
        private Token currentToken;
        private string currentText;

        /// <summary>
        /// Indicates the disposed state of the scanner.
        /// </summary>
        protected bool IsDisposed { get; private set; }

        /// <summary>
        /// Indicates the reading state of the scanner.
        /// </summary>
        protected ScannerState State { get; private set; }

        /// <summary>
        /// Gets last read token.
        /// </summary>
        /// <remarks><see cref="Token.Eof"/>, if there isn't tokens to read.</remarks>
        public virtual Token CurrentToken
        {
            get
            {
                ThrowIfDisposed();

                return currentToken;
            }
        }

        /// <summary>
        /// Gets last read token's text.
        /// </summary>
        public virtual string CurrentText
        {
            get
            {
                ThrowIfDisposed();

                return currentText;
            }
        }

        /// <summary>
        /// Initializes an instance of the <see cref="Scanner"/> class with the specified text reader.
        /// </summary>
        /// <param name="reader">The text reader.</param>
        /// <remarks>The textreader will be disposed at the scanner's <see cref="Dispose">dispose</see>.</remarks>
        public Scanner(string line)
        {
            if (line == null)
                throw new ArgumentNullException(nameof(line));

            this.reader = new StringReader(line);

            Initialize();
        }

        private void Initialize()
        {
            IsDisposed = false;
            State = ScannerState.Token;

            MoveNext();
        }

        /// <summary>
        /// Reads next token from the input stream, and updates the <see cref="CurrentToken"/> and the <see cref="CurrentText"/> properties.
        /// </summary>
        /// <remarks>
        /// If the end of stream found, the method sets <see cref="CurrentToken"/> to <see cref="Token.Eof"/> value.
        /// </remarks>
        public void MoveNext()
        {
            ThrowIfDisposed();

            var text = new StringBuilder();

            if (State == ScannerState.Token)
            {
                currentToken = ReadToken(text);
                if (currentToken == Token.Rem)
                    State = ScannerState.Comment;
            }
            else if (State == ScannerState.Comment)
            {
                currentToken = ReadComment(text);
                State = ScannerState.Token;
            }
            
            currentText = text.ToString();
        }

        private Token ReadToken(StringBuilder target)
        {
            reader.SkipWhile(char.IsWhiteSpace);

            if (reader.IsEof())
                return Token.Eof;

            Token token;
            if (TryReadBasicToken(target, out token))
                return token;

            var nextCharacter = (char)reader.Peek();
            var message = string.Format(ErrorMessages.UnexpectedCharacter, nextCharacter);
            throw new ParserException(message);
        }

        private bool TryReadBasicToken(StringBuilder target, out Token token)
        {
            if (reader.TryReadPunctuationMark(target, out token))
                return true;

            if (reader.TryReadOperator(target, out token))
                return true;

            if (reader.TryReadString(target, out token))
                return true;

            if (reader.TryReadIntegerOrFloatNumber(target, out token))
                return true;

            if (reader.TryReadIdentifierOrKeyword(target, out token))
                return true;

            return false;
        }

        private Token ReadComment(StringBuilder target)
        {
            reader.SkipWhile(char.IsWhiteSpace);
            reader.TakeWhile(c => true, target);

            return Token.Comment;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Disposes managed and unmanaged resources of the object.
        /// </summary>
        /// <param name="isDisposing"><c>true</c> when the method has been called by user's code;
        /// <c>false</c> when the method has been called by the runtime from inside the finalizer.</param>
        protected virtual void Dispose(bool isDisposing)
        {
            if (IsDisposed)
                return;

            if (isDisposing)
                reader.Dispose();

            IsDisposed = true;
        }

        /// <summary>
        /// Throws <see cref="ObjectDisposedException"/>, if the object is disposed.
        /// </summary>
        protected void ThrowIfDisposed()
        {
            if (IsDisposed)
            {
                var objectName = GetType().FullName;
                throw new ObjectDisposedException(objectName);
            }
        }
    }
}
