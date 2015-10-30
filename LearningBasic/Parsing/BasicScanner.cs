namespace LearningBasic.Parsing
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Implements BASIC scanner.
    /// </summary>
    public class BasicScanner : IScanner<Token>
    {
        /// <summary>
        /// Enumerates reading states.
        /// </summary>
        protected enum ScannerState
        {
            Token,

            Comment,
        };

        private readonly TextReader inputStream;
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
        /// Creates the instance of the BASIC scanner.
        /// </summary>
        /// <param name="inputStream">The input stream.</param>
        /// <remarks>The input stream will be disposed at the scanner's <see cref="Dispose">dispose</see>.</remarks>
        public BasicScanner(TextReader inputStream)
        {
            if (inputStream == null)
                throw new ArgumentNullException("inputStream");

            this.inputStream = inputStream;

            Initialize();
        }

        private void Initialize()
        {
            IsDisposed = false;
            State = ScannerState.Token;

            MoveNext();
        }

        /// <inheritdoc />
        public virtual void MoveNext()
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
            
            this.currentText = text.ToString();
        }

        private Token ReadToken(StringBuilder target)
        {
            this.inputStream.SkipWhile(char.IsWhiteSpace);

            if (inputStream.IsEof())
                return Token.Eof;

            Token token;
            if (TryReadBasicToken(target, out token))
                return token;

            var nextCharacterOfInputStream = (char)inputStream.Peek();
            throw new UnexpectedCharacterException(nextCharacterOfInputStream);
        }

        private bool TryReadBasicToken(StringBuilder target, out Token token)
        {
            if (inputStream.TryReadPunctuationMark(target, out token))
                return true;

            if (inputStream.TryReadOperator(target, out token))
                return true;

            if (inputStream.TryReadString(target, out token))
                return true;

            if (inputStream.TryReadIntegerOrFloatNumber(target, out token))
                return true;

            if (inputStream.TryReadIdentifierOrKeyword(target, out token))
                return true;

            return false;
        }

        private Token ReadComment(StringBuilder target)
        {
            inputStream.SkipWhile(char.IsWhiteSpace);
            inputStream.TakeWhile(c => true, target);

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
                inputStream.Dispose();

            IsDisposed = true;
        }

        /// <summary>
        /// Throws <see cref="ObjectDisposedException"/>, if the object is disposed.
        /// </summary>
        protected virtual void ThrowIfDisposed()
        {
            if (IsDisposed)
            {
                var objectName = GetType().FullName;
                throw new ObjectDisposedException(objectName);
            }
        }
    }
}
