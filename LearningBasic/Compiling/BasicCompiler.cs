namespace LearningBasic.Compiling
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq.Expressions;
    using System.Reflection;
    using LearningBasic.RunTime;
    using Microsoft.CSharp.RuntimeBinder;

    using CSharpBinder = Microsoft.CSharp.RuntimeBinder.Binder;

    /// <summary>
    /// Implements the <see cref="Command">command</see> builder (code generator).
    /// </summary>
    public class BasicCompiler : ICompiler<Tag>
    {
        public IStatement Compile(AstNode<Tag> statement)
        {
            var function = CompileToFunction(statement);
            return new BasicStatement(statement, function);
        }

        public Func<IRunTimeEnvironment, string> CompileToFunction(AstNode<Tag> statement)
        {
            switch (statement.Tag)
            {
                case Tag.Quit:
                    return (rte) => { rte.Close(); return string.Empty; };

                case Tag.Let:
                    return BuildLet(statement);

                case Tag.Print:
                    return BuildPrint(statement);

                case Tag.PrintLine:
                    return BuildPrintLine(statement);

                case Tag.Input:
                    return BuildInput(statement);

                default:
                    throw new CompilerException(string.Format(ErrorMessages.UnrecognizedStatement, statement.Tag));
            }
        }

        private static Func<IRunTimeEnvironment, string> BuildPrint(AstNode<Tag> statement)
        {
            return (rte) =>
            {
                foreach (var expressionNode in statement.Children)
                {
                    var expressionCommand = BuildCommandExpression(expressionNode);
                    var expression = expressionCommand(rte);
                    var convertibleExpression = Expression.Convert(expression, typeof(IConvertible));
                    var toStringMethod = typeof(IConvertible).GetMethod("ToString", new[] { typeof(IFormatProvider) });
                    var invariantCultureExpression = Expression.Constant(CultureInfo.InvariantCulture);
                    var stringExpression = Expression.Call(convertibleExpression, toStringMethod, invariantCultureExpression);
                    var getValue = Expression.Lambda<Func<string>>(stringExpression)
                                             .Compile();

                    rte.InputOutput.Write(getValue());
                }

                return string.Empty;
            };
        }

        private static Func<IRunTimeEnvironment, string> BuildPrintLine(AstNode<Tag> statement)
        {
            var printCommand = BuildPrint(statement);

            return (rte) =>
            {
                var result = printCommand(rte);
                rte.InputOutput.WriteLine();
                return result;
            };
        }

        private static Func<IRunTimeEnvironment, string> BuildInput(AstNode<Tag> statement)
        {
            return (rte) =>
            {
                rte.InputOutput.Write(statement.Text);
                var line = rte.InputOutput.ReadLine();
                var inputValues = new List<object>();

                foreach (var lValueNode in statement.Children)
                {
                    var lValueCommand = BuildCommandExpression(lValueNode);
                    var lValue = lValueCommand(rte);

                    int intValue;
                    double doubleValue;
                    Expression constant;

                    if (int.TryParse(line, NumberStyles.Integer, CultureInfo.InvariantCulture, out intValue))
                        constant = Expression.Constant(intValue);
                    else if (double.TryParse(line, NumberStyles.Float, CultureInfo.InvariantCulture, out doubleValue))
                        constant = Expression.Constant(doubleValue);
                    else
                        constant = Expression.Constant(line);
                    var objectConstant = Expression.Convert(constant, typeof(object));
                    var assignment = Expression.Assign(lValue, objectConstant);
                    var compiledAssignment = Expression.Lambda<Func<object>>(assignment)
                                                       .Compile();

                    inputValues.Add(compiledAssignment());
                }

                return string.Join(", ", inputValues);
            };
        }

        private delegate Expression CommandExpression(IRunTimeEnvironment runtimeSystem);
        private static readonly PropertyInfo dictionaryItemPropertyInfo = typeof(IDictionary<string, dynamic>).GetProperty("Item");

        public static Func<IRunTimeEnvironment, string> BuildLet(AstNode<Tag> statement)
        {
            var leftNode = statement.Children[0];
            var rightNode = statement.Children[1];
            var leftCommandExpression = BuildCommandExpression(leftNode);
            var rightCommandExpression = BuildCommandExpression(rightNode);

            return (rte) =>
            {
                var leftExpression = leftCommandExpression(rte);
                var rightExpression = rightCommandExpression(rte);
                var assignment = Expression.Assign(leftExpression, rightExpression);
                var compiledAssignment = Expression.Lambda<Func<object>>(assignment)
                                                   .Compile();
                var value = compiledAssignment();
                return value.ToString();
            };
        }

        private static CommandExpression BuildCommandExpression(AstNode<Tag> node)
        {
            switch (node.Tag)
            {
                case Tag.Integer:
                    return (rte) =>
                    {
                        var value = int.Parse(node.Text, NumberStyles.Integer, CultureInfo.InvariantCulture);
                        var valueAsConstantExpression = Expression.Constant(value);
                        var valueAsDynamicExpression = Expression.Convert(valueAsConstantExpression, typeof(object));
                        return valueAsDynamicExpression;
                    };

                case Tag.Real:
                    return (rte) =>
                    {
                        var value = double.Parse(node.Text, NumberStyles.Float, CultureInfo.InvariantCulture);
                        var valueAsConstantExpression = Expression.Constant(value);
                        var valueAsDynamicExpression = Expression.Convert(valueAsConstantExpression, typeof(object));
                        return valueAsDynamicExpression;
                    };

                case Tag.String:
                    return (rte) =>
                    {
                        var value = node.Text;
                        var valueAsConstantExpression = Expression.Constant(node.Text);
                        var valueAsDynamicExpression = Expression.Convert(valueAsConstantExpression, typeof(object));
                        return valueAsDynamicExpression;
                    };

                case Tag.Identifier:
                    return (rte) =>
                    {
                        var variables = Expression.Constant(rte.Variables);
                        var name = Expression.Constant(node.Text);
                        return Expression.MakeIndex(variables, dictionaryItemPropertyInfo, new[] { name });
                    };

                case Tag.Negative:
                    return BuildUnaryOperation(node, ExpressionType.Negate);

                case Tag.Positive:
                    return BuildUnaryOperation(node, ExpressionType.UnaryPlus);

                case Tag.Add:
                    return BuildBinaryOperation(node, ExpressionType.Add);

                case Tag.Subtract:
                    return BuildBinaryOperation(node, ExpressionType.Subtract);

                case Tag.Multiply:
                    return BuildBinaryOperation(node, ExpressionType.Multiply);

                case Tag.Divide:
                    return BuildBinaryOperation(node, ExpressionType.Divide);

                case Tag.Modulo:
                    return BuildBinaryOperation(node, ExpressionType.Modulo);

                case Tag.Power:
                    return BuildPowerOperation(node);

                default:
                    throw new Exception("Can't compile expression");
            }
        }

        private static CommandExpression BuildUnaryOperation(AstNode<Tag> operation, ExpressionType operationType)
        {
            return (rte) =>
            {
                var operandCommand = BuildCommandExpression(operation.Children[0]);
                var operand = operandCommand(rte);
                var operandInfos = new[]
                {
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, "operand"),
                };

                var binder = CSharpBinder.UnaryOperation(CSharpBinderFlags.None, operationType, typeof(BasicCompiler), operandInfos);
                return Expression.Dynamic(binder, typeof(object), operand);
            };
        }

        private static CommandExpression BuildBinaryOperation(AstNode<Tag> operation, ExpressionType operationType)
        {
            return (rte) =>
            {
                var leftOperandCommand = BuildCommandExpression(operation.Children[0]);
                var leftOperand = leftOperandCommand(rte);
                var rightOperandCommand = BuildCommandExpression(operation.Children[1]);
                var rightOperand = rightOperandCommand(rte);
                var operandInfos = new[]
                {
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, "operand1"),
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, "operand2"),
                };

                var binder = CSharpBinder.BinaryOperation(CSharpBinderFlags.None, operationType, typeof(BasicCompiler), operandInfos);
                return Expression.Dynamic(binder, typeof(object), leftOperand, rightOperand);
            };
        }

        private static CommandExpression BuildPowerOperation(AstNode<Tag> operation)
        {
            return (rte) =>
            {
                var leftOperandCommand = BuildCommandExpression(operation.Children[0]);
                var leftOperand = leftOperandCommand(rte);
                var rightOperandCommand = BuildCommandExpression(operation.Children[1]);
                var rightOperand = rightOperandCommand(rte);
                return Expression.Call(typeof(BuiltInFunctions).GetMethod("Power", new[] { typeof(object), typeof(object) }), leftOperand, rightOperand);
            };
        }
    }
}
