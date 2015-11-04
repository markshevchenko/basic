namespace LearningBasic.Parsing.Ast.Expressions
{
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public abstract class BinaryOperator : NaryExpression
    {
        public string Operator { get; private set; }

        public IExpression Left { get; private set; }

        public IExpression Right { get; private set; }

        protected BinaryOperator(Associativity associativity, Priority priority, string @operator, IExpression left, IExpression right)
            : base(associativity, priority)
        {
            Operator = @operator;
            Left = left;
            Right = right;
        }

        public override Expression GetExpression(IDictionary<string, dynamic> variables)
        {
            var left = Left.GetExpression(variables);
            var right = Right.GetExpression(variables);
            return Calculate(left, right);
        }

        protected abstract Expression Calculate(Expression left, Expression right);

        public override string ToString()
        {
            var left = Left.ToString();
            var right = Right.ToString();

            if (Left.Priority < Priority || (Left.Priority == Priority && Associativity == Associativity.Right))
                left = '(' + left + ')';

            if (Right.Priority < Priority || (Right.Priority == Priority && Associativity == Associativity.Left))
                right = '(' + right + ')';

            return string.Format("{0} {1} {2}", left, Operator, right);
        }
    }
}
