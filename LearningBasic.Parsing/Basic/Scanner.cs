namespace LearningBasic.Parsing.Basic
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Implements BASIC-specific scanner.
    /// </summary>
    public class Scanner : IScanner<Token>
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

        /// <inheritdoc />
        public virtual Token CurrentToken
        {
            get
            {
                ThrowIfDisposed();

                return currentToken;
            }
        }

        /// <inheritdoc />
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
        public Scanner(TextReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("inputStream");

            this.reader = reader;

            Initialize();
        }

        private void Initialize()
        {
            IsDisposed = false;
            State = ScannerState.Token;

            MoveNext();
        }

        /// <inheritdoc />
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
            Dispose(false);
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
