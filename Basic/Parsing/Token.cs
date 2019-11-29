namespace Basic.Parsing
{
    /// <summary>
    /// Specifies the BASIC tokens.
    /// </summary>
    public enum Token
    {
        // Service tokens

        Unknown = -1,
        Eof = 0,

        // Punctuation marks

        LParen,             // (
        RParen,             // )
        LBracket,           // [
        RBracket,           // ]
        Semicolon,          // ;
        Comma,              // ,

        // Conditions

        And,                // AND
        Not,                // NOT
        Or,                 // OR
        Eq,                 // =
        Lt,                 // <
        Le,                 // <=
        Ne,                 // <>
        Gt,                 // >
        Ge,                 // >=

        // Operators

        Plus,               // +
        Minus,              // -
        Asterisk,           // *
        Slash,              // /
        Caret,              // ^
        Mod,                // MOD

        // Terminals

        Identifier,         // ([a-zA-Z_][a-zA-Z0-9_]*)
        Integer,            // ([0-9]+)
        Float,              // ([0-9]+\.[0-9])
        String,             // "([^"]|"")*"
        Comment,            // ([^\n]*)

        // Keywords

        Dim,
        Else,
        End,
        For,
        Goto,
        If,
        Input,
        Let,
        List,
        Load,
        Next,
        Print,
        Quit,
        Randomize,
        Rem,
        Remove,
        Run,
        Save,
        Step,
        Then,
        To,
        Xor,
    }
}
