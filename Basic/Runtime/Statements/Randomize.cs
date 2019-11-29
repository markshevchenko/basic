namespace Basic.Runtime.Statements
{
    using System;

    public class Randomize : IStatement
    {
        public IExpression Seed { get; private set; }

        public Randomize(IExpression seed)
        {
            if (seed == null)
                throw new ArgumentNullException(nameof(seed));

            Seed = seed;
        }

        public EvaluateResult Execute(RunTimeEnvironment rte)
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
