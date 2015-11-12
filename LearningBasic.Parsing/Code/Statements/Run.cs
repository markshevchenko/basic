namespace LearningBasic.Parsing.Code.Statements
{
    using System;
    using LearningBasic.RunTime;

    public class Run : IStatement
    {
        public EvaluateResult Execute(IRunTimeEnvironment rte)
        {
            var result = rte.Run();
            if (result == ProgramResult.Broken)
                return new EvaluateResult(Messages.CtrlCPressed);

            if (result == ProgramResult.Completed)
                return new EvaluateResult(Messages.ProgramCompleted);

            throw new InvalidOperationException();
        }

        public override string ToString()
        {
            return "RUN";
        }
    }
}
