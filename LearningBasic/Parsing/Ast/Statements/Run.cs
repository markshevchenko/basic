namespace LearningBasic.Parsing.Ast.Statements
{
    using LearningBasic.Evaluating;

    public class Run : IStatement
    {
        public EvaluateResult Evaluate(IRunTimeEnvironment rte)
        {
            return rte.Run();
        }

        public override string ToString()
        {
            return "RUN";
        }
    }
}
