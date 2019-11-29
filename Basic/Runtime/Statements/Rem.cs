namespace Basic.Runtime.Statements
{
    public class Rem : IStatement
    {
        public string Comment { get; private set; }

        public Rem(string comment)
        {
            Comment = comment;
        }

        public EvaluateResult Execute(RunTimeEnvironment rte)
        {
            // REM does nothing.
            return EvaluateResult.None;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Comment))
                return "REM";

            return "REM " + Comment;
        }
    }
}
