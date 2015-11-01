namespace LearningBasic.Parsing
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class UnexpectedCharacterException : ParserException
    {
        public UnexpectedCharacterException(char character)
            : base(string.Format(ErrorMessages.UnexpectedCharacter, character))
        {
            Character = character;
        }

        protected UnexpectedCharacterException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Character = info.GetChar("Character");
        }

        public char Character { get; private set; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Character", Character);
        }
    }
}
