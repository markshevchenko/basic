namespace LearningBasic.Parsing.Ast.Statements
{
    using System;

    public class Run : IStatement
    {
        public EvaluateResult Execute(IRunTimeEnvironment rte)
        {
            var result = rte.Run();
            if (result.IsBroken)
                return new EvaluateResult(Messages.CtrlCPressed);

            if (result.IsCompleted)
                return new EvaluateResult(Messages.ProgramCompleted);

            if (result.IsAborted)
            {
                var message = string.Format(ErrorMessages.RunTimeErrorOccured, result.Exception.Message);
                return new EvaluateResult(message);
            }

            throw new InvalidOperationException();
        }

        public override string ToString()
        {
            return "RUN";
        }
    }
}
