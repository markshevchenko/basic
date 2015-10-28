namespace LearningBasic.Parsing
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Incapsulates syntax rule.
    /// </summary>
    public class SyntaxRule<TToken, TTag>
        where TToken : struct
        where TTag : struct
    {
        private readonly IScanner<TToken> scanner;

        public bool IsSuccessful { get; private set; }

        public SyntaxRule(IScanner<TToken> scanner, TToken token)
        {
            this.scanner = scanner;
            this.IsSuccessful = scanner.TryReadToken(token);
        }

        public SyntaxRule(IScanner<TToken> scanner, TToken token, out string text)
        {
            this.scanner = scanner;
            this.IsSuccessful = scanner.TryReadToken(token, out text);
        }

        public SyntaxRule<TToken, TTag> Let(TToken token)
        {
            if (this.IsSuccessful)
                this.scanner.TryReadToken(token);

            return this;
        }

        public SyntaxRule<TToken, TTag> Let(TToken token, out string text)
        {
            if (this.IsSuccessful)
                this.scanner.TryReadToken(token, out text);
            else
                text = null;

            return this;
        }

        public SyntaxRule<TToken, TTag> Let(Func<IScanner<TToken>, AstNode<TTag>> parser, out AstNode<TTag> node)
        {
            if (this.IsSuccessful)
                this.scanner.TryReadNode(parser, out node);
            else
                node = null;

            return this;
        }

        public SyntaxRule<TToken, TTag> Let(Func<IScanner<TToken>, IReadOnlyList<AstNode<TTag>>> listParser, out IReadOnlyList<AstNode<TTag>> nodeList)
        {
            if (this.IsSuccessful)
                nodeList = listParser(this.scanner) ?? new List<AstNode<TTag>>();
            else
                nodeList = new List<AstNode<TTag>>();

            return this;
        }

        public SyntaxRule<TToken, TTag> Require(TToken token, string exceptionFormat, params object[] exceptionArgs)
        {
            if (this.IsSuccessful)
            {
                if (!this.scanner.TryReadToken(token))
                    throw new ParserException(string.Format(exceptionFormat, exceptionArgs));
            }

            return this;
        }

        public SyntaxRule<TToken, TTag> Require(TToken token, out string text, string exceptionFormat, params object[] exceptionArgs)
        {
            if (this.IsSuccessful)
            {
                if (!this.scanner.TryReadToken(token, out text))
                    throw new ParserException(string.Format(exceptionFormat, exceptionArgs));
            }
            else
                text = null;

            return this;
        }

        public SyntaxRule<TToken, TTag> Requre(Func<IScanner<TToken>, AstNode<TTag>> parser, out AstNode<TTag> node, string exceptionFormat, params object[] exceptionArgs)
        {
            if (this.IsSuccessful)
            {
                if (!this.scanner.TryReadNode(parser, out node))
                    throw new ParserException(string.Format(exceptionFormat, exceptionArgs));
            }
            else
                node = null;

            return this;
        }

        public SyntaxRule<TToken, TTag> Require(Func<IScanner<TToken>, IReadOnlyList<AstNode<TTag>>> listParser, out IReadOnlyList<AstNode<TTag>> nodeList, string exceptionFormat, params object[] exceptionArgs)
        {
            if (this.IsSuccessful)
            {
                nodeList = listParser(this.scanner);

                if (nodeList == null || nodeList.Count == 0)
                    throw new ParserException(string.Format(exceptionFormat, exceptionArgs));
            }
            else
                nodeList = new List<AstNode<TTag>>();

            return this;
        }
    }
}
