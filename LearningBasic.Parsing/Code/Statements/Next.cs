﻿namespace LearningBasic.Parsing.Code.Statements
{
    using LearningBasic.RunTime;

    public class Next : IStatement
    {
        public EvaluateResult Execute(IRunTimeEnvironment rte)
        {
            rte.TakeLastLoopStep();

            if (rte.IsLastLoopOver)
                rte.StopLastLoop();
            else
                rte.RepeatLastLoop();

            return EvaluateResult.None;
        }
        public override string ToString()
        {
            return "NEXT";
        }
    }
}
