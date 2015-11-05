namespace LearningBasic.Parsing.Ast.Expressions
{
    using System.Linq.Expressions;

    public class Divide : BinaryOperator
    {
        public Divide(IExpression left, IExpression right)
            : base(Associativity.Left, Priority.ArithmeticMultiplication, "/", left, right)
        {
            DoInsertSpacebar = false;
        }

        protected override Expression BuildExpression(Expression left, Expression right)
        {
            return CallStaticMethod(typeof(BuiltInOperators), "Divide", left, right);
        }
    }
}
