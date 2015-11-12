namespace LearningBasic.Parsing.Code.Statements
{
    using System;
    using LearningBasic.RunTime;

    public class Randomize : IStatement
    {
        public IExpression Seed { get; private set; }

        public Randomize(IExpression seed)
        {
            if (seed == null)
                throw new ArgumentNullException("seed");

            Seed = seed;
        }

        public EvaluateResult Execute(IRunTimeEnvironment rte)
        {
            var seedException = Seed.GetExpression(rte.Variables);
            var seed = seedException.Calculate();
            rte.Randomize((int)seed);

            return new EvaluateResult(Messages.RandomizeSeedAccepted, seed);
        }

        public override string ToString()
        {
            return "RANDOMIZE " + Seed;
        }
    }
}
