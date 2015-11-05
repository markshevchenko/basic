namespace LearningBasic.Parsing.Ast.Statements
{
    public class Rem : IStatement
    {
        public string Comment { get; private set; }

        public Rem(string comment)
        {
            Comment = comment;
        }

        public EvaluateResult Execute(IRunTimeEnvironment rte)
        {
            // REM does nothing.
            return EvaluateResult.Empty;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Comment))
                return "REM";

            return "REM " + Comment;
        }
    }
}
