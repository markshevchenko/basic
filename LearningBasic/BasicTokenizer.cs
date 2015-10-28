using System;
using System.Collections.Generic;
using System.IO;
using Binateq.Parsing.Lexical;

namespace Basic
{
    public class BasicTokenizer : MultiStateTokenizer<Token, BasicTokenizerState>
    {
        public BasicTokenizer(TextReader inputStream)
            : base(inputStream)
        { }

        protected override void CreateStateRules(IDictionary<BasicTokenizerState, LexicalRule<Token, BasicTokenizerState>> stateRules)
        {
            stateRules.Add(BasicTokenizerState.DEFAULT, defaultRecognizer);
            stateRules.Add(BasicTokenizerState.COMMENT, commentRecognizer);
            stateRules.Add(BasicTokenizerState.STRING, stringRecognizer);
        }

        private static readonly LexicalRule<Token, BasicTokenizerState> defaultRecognizer =
            // Delimiters
            Rule(EOF, ReturnToken(Token.EOF))
          | Rule(TakeOneOrMore(c => c == ' ' || c == '\t'), None)
          | Rule('\n', ReturnToken(Token.EOL))

            // Punctuation marks
          | Rule('(', ReturnToken(Token.LPAREN))
          | Rule(')', ReturnToken(Token.RPAREN))
          | Rule('[', ReturnToken(Token.LBRACKET))
          | Rule(']', ReturnToken(Token.RBRACKET))
          | Rule(';', ReturnToken(Token.SEMICOLON))
          | Rule(',', ReturnToken(Token.COMMA))

            // Operators
          | Rule('+', ReturnToken(Token.PLUS))
          | Rule('-', ReturnToken(Token.MINUS))
          | Rule('*', ReturnToken(Token.ASTERISK))
          | Rule('/', ReturnToken(Token.SLASH))
          | Rule('%', ReturnToken(Token.PERCENT))
          | Rule('^', ReturnToken(Token.CARET))

            // Relations
          | Rule('=', ReturnToken(Token.EQ))
          | Rule('<', ReturnToken(Token.LT)) & (Rule('>', ReturnToken(Token.NE)) | Rule('=', ReturnToken(Token.LE)))
          | Rule('>', ReturnToken(Token.GT)) & Rule('=', ReturnToken(Token.GE))

            // Identifiers, keywords
          | Rule(Take(c => char.IsLetter(c) || c == '_') & TakeZeroOrMore(c => char.IsLetterOrDigit(c) || c == '_'), (buffer) =>
              {
                  var text = buffer.ToString();

                  if (keywords.ContainsKey(text))
                  {
                      var keyword = keywords[text];
                      if (keyword == Token.REM)
                          return TokenState(keyword, BasicTokenizerState.COMMENT);

                      return ReturnToken(keyword);
                  }

                  return ReturnToken(Token.IDENTIFIER);
              })
          | Rule(TakeOneOrMore(char.IsDigit), ReturnToken(Token.INTEGER)) & Rule(Take('.') & TakeZeroOrMore(char.IsDigit), ReturnToken(Token.REAL))
            ;

        private static readonly LexicalRule<Token, BasicTokenizerState> commentRecognizer =
            Rule(SkipOneOrMore(c => c == ' ' || c == '\n') & TakeZeroOrMore(c => c != '\n'), TokenState(Token.COMMENT, BasicTokenizerState.DEFAULT));

        private static readonly LexicalRule<Token, BasicTokenizerState> stringRecognizer =
            null
            ;

        private static readonly IDictionary<string, Token> keywords = new SortedList<string, Token>(StringComparer.InvariantCultureIgnoreCase)
        {
            { "AND", Token.AND },
            { "DIM", Token.DIM },
            { "ELSE", Token.ELSE },
            { "END", Token.END },
            { "FOR", Token.FOR },
            { "GOTO", Token.GOTO },
            { "IF", Token.IF },
            { "INPUT", Token.INPUT },
            { "LET", Token.LET },
            { "LIST", Token.LIST },
            { "LOAD", Token.LOAD },
            { "NEXT", Token.NEXT },
            { "NOT", Token.NOT },
            { "OR", Token.OR },
            { "PRINT", Token.PRINT },
            { "QUIT", Token.QUIT },
            { "RANDOMIZE", Token.RANDOMIZE },
            { "REM", Token.REM },
            { "REMOVE", Token.REMOVE },
            { "RUN", Token.RUN },
            { "SAVE", Token.SAVE },
            { "STEP", Token.STEP },
            { "THEN", Token.THEN },
            { "TO", Token.TO },
            { "XOR", Token.XOR },
        };
    }
}
