﻿namespace Basic.Runtime.Conditions
{
    using System.Linq.Expressions;

    public class And : BinaryOperator
    {
        public And(IExpression left, IExpression right)
            : base(Associativity.Left, Priority.LogicalMultiplication, "AND", left, right)
        { }

        protected override Expression BuildExpression(Expression left, Expression right)
        {
            return DynamicBuilder.BuildLogicalAnd(left, right);
        }
    }
}
