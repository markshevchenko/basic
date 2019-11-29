namespace Basic.Runtime.Statements
{
    using System;
    using System.Linq.Expressions;
    using Basic.Runtime.Expressions;

    public class Let : IStatement
    {
        public ILValue Left { get; private set; }

        public IExpression Right { get; private set; }

        public Let(ILValue left, IExpression right)
        {
            if (left == null)
                throw new ArgumentNullException(nameof(left));

            if (right == null)
                throw new ArgumentNullException(nameof(right));

            Left = left;
            Right = right;
        }

        public EvaluateResult Execute(RunTimeEnvironment rte)
        {
            var left = Left.GetExpression(rte.Variables);
            var right = Right.GetExpression(rte.Variables);
            var rightAsObject = Expression.Convert(right, typeof(object));
            var assignment = Expression.Assign(left, rightAsObject);
            var value = assignment.Calculate();
            var result = Constant.ToString(value);
            return new EvaluateResult(result);
        }

        public override string ToString()
        {
            return string.Format("LET {0} = {1}", Left, Right);
        }
    }
}
