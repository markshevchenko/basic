namespace LearningBasic.Parsing.Ast.Expressions
{
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public abstract class UnaryOperator : NaryExpression
    {
        public string Operator { get; private set; }

        public IExpression Operand { get; private set; }

        protected UnaryOperator(Associativity associativity, Priority priority, string @operator, IExpression operand)
            : base(associativity, priority)
        {
            Operator = @operator;
            Operand = operand;
        }

        public override Expression GetExpression(IDictionary<string, dynamic> variables)
        {
            var operand = Operand.GetExpression(variables);
            return Calculate(operand);
        }

        protected abstract Expression Calculate(Expression operand);

        public override string ToString()
        {
            var operand = Operand.ToString();

            if (Priority > Operand.Priority)
                operand = '(' + operand + ')';

            return string.Format("{0}{1}", Operator, operand);
        }
    }
}
