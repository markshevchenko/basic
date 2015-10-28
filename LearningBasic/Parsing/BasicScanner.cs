namespace Basic.Parsing
{
    using System;
    using System.Collections.Generic;
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

                return this.currentToken;
            }
        }

        /// <inheritdoc />
        public virtual string CurrentText
        {
            get
            {
                ThrowIfDisposed();

                return this.currentText;
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
            this.IsDisposed = false;
            this.State = ScannerState.Token;

            this.MoveNext();
        }

        /// <inheritdoc />
        public virtual void MoveNext()
        {
            ThrowIfDisposed();

            var text = new StringBuilder();

            if (this.State == ScannerState.Token)
            {
                this.currentToken = this.ReadToken(text);
                if (this.currentToken == Token.Rem)
                    this.State = ScannerState.Comment;
            }
            else if (this.State == ScannerState.Comment)
            {
                this.currentToken = this.ReadComment(text);
                this.State = ScannerState.Token;
            }
            
            this.currentText = text.ToString();
        }

        private Token ReadToken(StringBuilder target)
        {
            this.inputStream.SkipWhile(char.IsWhiteSpace);

            if (this.inputStream.IsEof())
                return Token.Eof;

            Token token;
            if (this.TryReadBasicToken(target, out token))
                return token;

            var nextCharacterOfInputStream = (char)this.inputStream.Peek();
            throw new UnexpectedCharacterException(nextCharacterOfInputStream);
        }

        private bool TryReadBasicToken(StringBuilder target, out Token token)
        {
            if (this.inputStream.TryReadPunctuationMark(target, out token))
                return true;

            if (this.inputStream.TryReadOperator(target, out token))
                return true;

            if (this.inputStream.TryReadString(target, out token))
                return true;

            if (this.inputStream.TryReadIntegerOrFloatNumber(target, out token))
                return true;

            if (this.inputStream.TryReadIdentifierOrKeyword(target, out token))
                return true;

            return false;
        }

        private Token ReadComment(StringBuilder target)
        {
            this.inputStream.SkipWhile(char.IsWhiteSpace);
            this.inputStream.TakeWhile(c => true, target);

            return Token.Comment;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Disposes managed and unmanaged resources of the object.
        /// </summary>
        /// <param name="isDisposing"><c>true</c>, if called from <see cref="Dispose"/> method;
        /// <c>false</c>, if called from finalizator.</param>
        protected virtual void Dispose(bool isDisposing)
        {
            if (this.IsDisposed)
                return;

            if (isDisposing)
                this.inputStream.Dispose();

            this.IsDisposed = true;
        }

        /// <summary>
        /// Throws <see cref="ObjectDisposedException"/>, if the object is disposed.
        /// </summary>
        protected virtual void ThrowIfDisposed()
        {
            if (this.IsDisposed)
            {
                var objectName = this.GetType().FullName;
                throw new ObjectDisposedException(objectName);
            }
        }
    }
}
