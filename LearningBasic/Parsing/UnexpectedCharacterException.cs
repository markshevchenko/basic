namespace Basic.Parsing
{
    using System;

    public class UnexpectedCharacterException : ParserException
    {
        public UnexpectedCharacterException(char character)
            : base(string.Format(ErrorMessages.UnexpectedCharacter, character))
        {
            Character = character;
        }

        public char Character { get; private set; }
    }
}
