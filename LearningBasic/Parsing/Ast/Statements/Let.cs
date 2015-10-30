namespace LearningBasic.Parsing.Ast.Statements
{
    using System;
    using System.Linq.Expressions;

    public class Let : IStatement
    {
        public ILValue Left { get; private set; }

        public IExpression Right { get; private set; }

        public Let(ILValue left, IExpression right)
        {
            if (left == null)
                throw new ArgumentNullException("left");

            if (right == null)
                throw new ArgumentNullException("right");

            Left = left;
            Right = right;
        }

        public Result Run(IRunTimeEnvironment rte)
        {
            var left = Left.Compile(rte);
            var right = Right.Compile(rte);
            var assignment = Expression.Assign(left, right);
            var value = assignment.CalculateValue();
            return new Result(value);
        }

        public override string ToString()
        {
            return string.Format("LET {0} = {1}", Left, Right);
        }
    }
}
