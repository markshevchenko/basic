namespace LearningBasic.Parsing.Ast.Statements
{
    public class Next : IStatement
    {
        public EvaluateResult Execute(IRunTimeEnvironment rte)
        {
            rte.TakeLastMultilineLoopStep();

            return EvaluateResult.None;
        }
        public override string ToString()
        {
            return "NEXT";
        }
    }
}
