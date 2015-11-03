namespace LearningBasic.Parsing
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// The exception that is thrown when unexpected character read.
    /// </summary>
    [Serializable]
    public class UnexpectedCharacterException : ParserException
    {
        /// <summary>
        /// Gets the unexpected character.
        /// </summary>
        public char Character { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnexpectedCharacterException"/> class
        /// with the unexpected character.
        /// </summary>
        /// <param name="character">The unexpected character.</param>
        public UnexpectedCharacterException(char character)
            : base(string.Format(ErrorMessages.UnexpectedCharacter, character))
        {
            Character = character;
        }

        /// <inheritdoc />
        protected UnexpectedCharacterException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Character = info.GetChar("Character");
        }

        /// <inheritdoc />
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Character", Character);
        }
    }
}
