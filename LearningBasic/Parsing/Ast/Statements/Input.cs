namespace LearningBasic.Parsing.Ast.Statements
{
    using System;
    using System.Globalization;
    using System.Linq.Expressions;

    public class Input : IStatement
    {
        public string Prompt { get; }

        public ILValue LValue { get; }

        public Input(ILValue lValue)
        {
            if (lValue == null)
                throw new ArgumentNullException("lValue");

            Prompt = null;
            LValue = lValue;
        }

        public Input(string prompt, ILValue lValue)
        {
            if (prompt == null)
                throw new ArgumentNullException("prompt");

            if (lValue == null)
                throw new ArgumentNullException("lValue");

            Prompt = prompt;
            LValue = lValue;
        }

        public EvaluateResult Run(IRunTimeEnvironment rte)
        {
            rte.InputOutput.Write(Prompt);
            var line = rte.InputOutput.ReadLine();

            var rValue = ParseToObjectExpression(line);
            var lValue = LValue.GetExpression(rte);
            var assignment = Expression.Assign(lValue, rValue);

            var value = assignment.CalculateValue();
            return new EvaluateResult(value);
        }

        public override string ToString()
        {
            if (Prompt == null)
                return "INPUT " + LValue.ToString();
            else
                return "INPUT " + Prompt.ToListing() + ", " + LValue.ToString();
        }

        public static Expression ParseToObjectExpression(string s)
        {
            var constantExpression = ParseToConstantExpression(s);
            return Expression.Convert(constantExpression, typeof(object));
        }

        public static ConstantExpression ParseToConstantExpression(string s)
        {
            int i;
            if (int.TryParse(s, NumberStyles.None, CultureInfo.InvariantCulture, out i))
                return Expression.Constant(i);

            double d;
            if (double.TryParse(s, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out d))
                return Expression.Constant(d);

            return Expression.Constant(s);
        }
    }
}
