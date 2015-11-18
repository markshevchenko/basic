namespace LearningInterpreter.Basic.Code.Statements
{
    using LearningInterpreter.RunTime;

    public class Quit : IStatement
    {
        public EvaluateResult Execute(IRunTimeEnvironment rte)
        {
            rte.Close();

            return new EvaluateResult(Messages.BasicInterpreterTerminated);
        }

        public override string ToString()
        {
            return "QUIT";
        }
    }
}
