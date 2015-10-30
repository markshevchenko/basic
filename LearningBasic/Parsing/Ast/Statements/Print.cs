﻿namespace LearningBasic.Parsing.Ast.Statements
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class Print : IStatement
    {
        public IReadOnlyList<IExpression> Expressions { get; private set; }

        public Print(IEnumerable<IExpression> expressions)
        {
            if (expressions == null)
                throw new ArgumentNullException("expressions");

            Expressions = new List<IExpression>(expressions);
        }

        public virtual Result Run(IRunTimeEnvironment rte)
        {
            foreach (var expression in Expressions)
            {
                var e = expression.Compile(rte);
                rte.InputOutput.Write(e.ToString(CultureInfo.InvariantCulture));
            }

            return Result.Nothing;
        }

        public override string ToString()
        {
            return "PRINT " + string.Join(", ", Expressions);
        }
    }
}
