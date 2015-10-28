using System;
using System.Collections.Generic;
using System.IO;
using Binateq.Parsing.Syntax;

namespace Basic
{
    using Node = AstNode<Tag>;
    using NodeRule = Production<Token, Tag>;
    using System.Collections;

    public class BasicParser : Parser<Token, Tag>
    {
        public BasicParser(TextReader inputStream)
            : base(new BasicTokenizer(inputStream), StartRule)
        { }

        private static NodeRule StartRule()
        {
            return Rule.Start(Lines)
                       .RequireIgnoringText(Token.EOF)
                       .Return((lines) => lines);
        }

        private static NodeRule Lines()
        {
            return Rule.Start(Line)
                       .Let(
                            Rule.StartIgnoringText(Token.EOL)
                                .Require(Lines)
                                .Return()
                        )
                       .Return((line, lines) => Node.Cons(Tag.Line, line, lines));
        }

        private static NodeRule Line()
        {
            return Rule.Start(Token.INTEGER)
                       .Require(
                           Rule.UseIgnoringText(Token.NEXT, Tag.Next)
                         | Rule.Use(Statement)
                         , ErrorMessages.MissingStatement
                        )
                       .Return((lineNumber, statement) => new Node(Tag.Line, lineNumber, statement))

                 | Rule.Start(Statement)
                       .Return(statement => new Node(Tag.Line, statement))

                 | Rule.UseIgnoringText(Token.NEXT, Tag.Next);
        }

        private static NodeRule Statement()
        {
            return Rule.Use(Dim)
                 | Rule.Use(Randomize)
                 | Rule.Use(Print)
                 | Rule.Use(Input)
                 | Rule.Use(If)
                 | Rule.Use(For)
                 | Rule.Use(Goto)
                 | Rule.Use(Remove)
                 | Rule.Use(List)
                 | Rule.Use(Save)
                 | Rule.Use(Load)
                 | Rule.Use(Rem)
                 | Rule.UseIgnoringText(Token.RUN, Tag.Run)
                 | Rule.UseIgnoringText(Token.END, Tag.End)
                 | Rule.UseIgnoringText(Token.QUIT, Tag.Quit)
                 | Rule.Use(Let);
        }

        private static NodeRule Dim()
        {
            return Rule.StartIgnoringText(Token.DIM)
                       .Require(Token.IDENTIFIER)
                       .Require(ArraySuffix, ErrorMessages.MissingArray)
                       .Return((identifier, array) => new Node(Tag.LValue, identifier.ToUpper(), array));
        }

        private static NodeRule Randomize()
        {
            return Rule.StartIgnoringText(Token.RANDOMIZE)
                       .Let(Expression)
                       .Return(expression => new Node(Tag.Randomize, expression));
        }

        private static NodeRule Print()
        {
            return Rule.StartIgnoringText(Token.PRINT)
                       .Let(Expressions)
                       .Let(Token.SEMICOLON)
                       .Return((expressions, semicolon) =>
                        {
                            var tag = semicolon == null ? Tag.Print : Tag.PrintLine;
                            
                            if (expressions == null)
                            {
                                // It's disable to use "PRINT ;" cause it does nothing
                                if (tag == Tag.Print)
                                    throw new SyntaxErrorException(ErrorMessages.InvalidSemicolonInPrint);

                                return new Node(Tag.PrintLine);
                            }

                            return new Node(tag, expressions);
                        });
        }

        private static NodeRule Input()
        {
            return Rule.StartIgnoringText(Token.INPUT)
                       .Let(
                            Rule.Start(Token.STRING)
                                .RequireIgnoringText(Token.COMMA)
                                .Return((prompt) => new Node(Tag.String, prompt))
                        )
                       .Require(LValues, ErrorMessages.MissingLValue)
                       .Return((prompt, lValues) =>
                        {
                            if (prompt == null)
                                return new Node(Tag.Input, lValues);

                            return new Node(Tag.Input, prompt.Text, lValues);
                        });
        }

        private static NodeRule If()
        {
            return Rule.StartIgnoringText(Token.IF)
                       .Require(Condition, ErrorMessages.MissingCondition)
                       .RequireIgnoringText(Token.THEN)
                       .Require(Statement, ErrorMessages.MissingStatement)
                       .Let(
                            Rule.StartIgnoringText(Token.ELSE)
                                .Require(Statement, ErrorMessages.MissingStatement)
                                .Return()
                        )
                       .Return((condition, thenStatement, elseStatement) =>
                        {
                            return new Node(Tag.If, condition, thenStatement, elseStatement);
                        });
        }

        private static NodeRule For()
        {
            return null;
            /*
            if (MissingNext(BasicToken.FOR))
                return null;

            var nodes = new List<NodeRule>();
            nodes.Add(RequireNext(LValueOrFunction(true), ErrorMessages.MissingRValue));
            RequireNext(BasicToken.EQ);
            nodes.Add(RequireNext(Expression(), ErrorMessages.MissingExpression));
            RequireNext(BasicToken.TO);
            nodes.Add(RequireNext(Expression(), ErrorMessages.MissingExpression));

            if (TryNext(BasicToken.STEP))
            {
                var stepExpression = RequireNext(Expression(), ErrorMessages.MissingExpression);
                nodes.Add(new Unary(BasicToken.STEP, stepExpression));
            }

            var statement = StatementOld();
            if (statement != null)
            {
                if (statement.Tag == BasicToken.NEXT)
                    throw new SyntaxErrorException(ErrorMessages.MissingStatement);

                RequireNext(BasicToken.NEXT);
                nodes.Add(new Unary(BasicToken.INLINE_FOR, statement));
            }

            return new Multary(BasicToken.FOR, nodes);
             */
        }

        private static NodeRule Goto()
        {
            return Rule.StartIgnoringText(Token.GOTO)
                       .Require(Expression, ErrorMessages.MissingExpression)
                       .Return(expression => new Node(Tag.Goto, expression));
        }

        private static NodeRule Remove()
        {
            return Rule.StartIgnoringText(Token.REMOVE)
                       .Let(Range)
                       .Return(range => new Node(Tag.Remove, range));
        }

        private static NodeRule List()
        {
            return Rule.StartIgnoringText(Token.LIST)
                       .Let(Range)
                       .Return(range => new Node(Tag.List, range));
        }

        private static NodeRule Save()
        {
            return Rule.StartIgnoringText(Token.SAVE)
                       .Let(Token.STRING)
                       .Return(fileName => new Node(Tag.Save, fileName));
        }

        private static NodeRule Load()
        {
            return Rule.StartIgnoringText(Token.LOAD)
                       .Require(Token.STRING)
                       .Return(fileName => new Node(Tag.Load, fileName));
        }

        private static NodeRule Rem()
        {
            return Rule.StartIgnoringText(Token.REM)
                       .Require(Token.COMMENT)
                       .Return(comment => new Node(Tag.Rem, comment));
        }

        private static NodeRule Let()
        {
            return Rule.StartIgnoringText(Token.LET)
                       .Require(Assignment, ErrorMessages.MissingAssignment)
                       .Return()

                 | Rule.Start(Assignment)
                       .Return();
        }

        private static NodeRule Assignment()
        {
            return Rule.Start(LValue)
                       .RequireIgnoringText(Token.EQ)
                       .Require(Expression, ErrorMessages.MissingExpression)
                       .Return((lValue, expression) => new Node(Tag.Let, lValue, expression));
        }

        private static NodeRule Range()
        {
            return Rule.Start(Token.INTEGER)
                       .Let(
                            Rule.StartIgnoringText(Token.MINUS)
                                .Require(Token.INTEGER)
                                .Return(integer => new Node(Tag.Integer, integer))
                        )
                        .Return((startInteger, end) => new Node(Tag.Range, new Node(Tag.Integer, startInteger), end));
        }

        private static NodeRule Condition()
        {
            return Rule.Start(OrOperands)
                       .Return(operands => operands);
        }

        private static NodeRule OrOperands()
        {
            return Rule.Start(OrOperand)
                       .Let(
                            Rule.StartIgnoringText(Token.OR)
                                .Require(OrOperands)
                                .Return(tail => Node.MakeOperatorAndRightOperand(Tag.Or, tail))

                          | Rule.StartIgnoringText(Token.XOR)
                                .Require(OrOperands)
                                .Return(tail => Node.MakeOperatorAndRightOperand(Tag.Xor, tail))
                        )
                        .Return((left, operatorAndRight) => Node.MakeLeftAssociativeExpression(left, operatorAndRight, Tag.Or, Tag.Xor));
        }

        private static NodeRule OrOperand()
        {
            return Rule.Use(AndOperands);
        }

        private static NodeRule AndOperands()
        {
            return Rule.Start(AndOperand)
                       .Let(
                            Rule.StartIgnoringText(Token.AND)
                                .Require(AndOperands)
                                .Return(tail => Node.MakeOperatorAndRightOperand(Tag.And, tail))
                        )
                        .Return((left, operatorAndRight) => Node.MakeLeftAssociativeExpression(left, operatorAndRight));
        }

        private static NodeRule AndOperand()
        {
            return Rule.Use(NotOperand);
        }

        private static NodeRule NotOperand()
        {
            return Rule.StartIgnoringText(Token.NOT)
                       .Require(NotOperand)
                       .Return(operand => new Node(Tag.Not, operand))

                 | Rule.Use(Relation);
        }

        private static NodeRule Relation()
        {
            return Rule.Start(Expression)
                       .Require(
                            Rule.StartIgnoringText(Token.EQ)
                                .Require(Expression, ErrorMessages.MissingExpression)
                                .Return(expression => new Node(Tag.Eq, expression))

                          | Rule.StartIgnoringText(Token.NE)
                                .Require(Expression, ErrorMessages.MissingExpression)
                                .Return(expression => new Node(Tag.Ne, expression))

                          | Rule.StartIgnoringText(Token.LT)
                                .Require(Expression, ErrorMessages.MissingExpression)
                                .Return(expression => new Node(Tag.Lt, expression))

                          | Rule.StartIgnoringText(Token.LE)
                                .Require(Expression, ErrorMessages.MissingExpression)
                                .Return(expression => new Node(Tag.Le, expression))

                          | Rule.StartIgnoringText(Token.GT)
                                .Require(Expression, ErrorMessages.MissingExpression)
                                .Return(expression => new Node(Tag.Gt, expression))

                          | Rule.StartIgnoringText(Token.GE)
                                .Require(Expression, ErrorMessages.MissingExpression)
                                .Return(expression => new Node(Tag.Ge, expression))

                          , ErrorMessages.MissingRelationOperator
                        )
                       .Return((left, operatorAndRight) => new Node(operatorAndRight.Tag, left, operatorAndRight[0]));
        }

        private static NodeRule Expressions()
        {
            return Rule.Start(Expression)
                       .Let(
                            Rule.StartIgnoringText(Token.COMMA)
                                .Require(Expressions, ErrorMessages.MissingExpression)
                                .Return()
                        )
                       .Return((expression, expressions) => Node.Cons(Tag.Expression, expression, expressions));
        }

        private static NodeRule Expression()
        {
            return Rule.Start(AddOperands)
                       .Return();
        }

        private static NodeRule AddOperands()
        {
            return Rule.Start(AddOperand)
                       .Let(
                            Rule.StartIgnoringText(Token.PLUS)
                                .Require(AddOperands)
                                .Return(tail => Node.MakeOperatorAndRightOperand(Tag.Add, tail))

                          | Rule.StartIgnoringText(Token.MINUS)
                                .Require(AddOperands)
                                .Return(tail => Node.MakeOperatorAndRightOperand(Tag.Subtract, tail))
                        )
                        .Return((left, operatorAndRight) => Node.MakeLeftAssociativeExpression(left, operatorAndRight, Tag.Add, Tag.Subtract));
        }

        private static NodeRule AddOperand()
        {
            return Rule.Use(MulOperands);
        }

        private static NodeRule MulOperands()
        {
            return Rule.Start(MulOperand)
                       .Let(
                            Rule.StartIgnoringText(Token.ASTERISK)
                                .Require(MulOperands)
                                .Return(tail => Node.MakeOperatorAndRightOperand(Tag.Multiply, tail))

                          | Rule.StartIgnoringText(Token.SLASH)
                                .Require(MulOperands)
                                .Return(tail => Node.MakeOperatorAndRightOperand(Tag.Divide, tail))

                          | Rule.StartIgnoringText(Token.PERCENT)
                                .Require(MulOperands)
                                .Return(tail => Node.MakeOperatorAndRightOperand(Tag.Remainder, tail))
                         )
                       .Return((left, operatorAndRight) => Node.MakeLeftAssociativeExpression(left, operatorAndRight, Tag.Multiply, Tag.Divide, Tag.Remainder));
        }

        private static NodeRule MulOperand()
        {
            return Rule.Use(UnOperand);
        }

        private static NodeRule UnOperand()
        {
            return Rule.StartIgnoringText(Token.MINUS)
                       .Require(UnOperand)
                       .Return(operand => new Node(Tag.Negative, operand, null))

                 | Rule.StartIgnoringText(Token.PLUS)
                       .Require(UnOperand)
                       .Return(operand => new Node(Tag.Positive, operand, null))

                 | Rule.Use(PowOperands);
        }

        private static NodeRule PowOperands()
        {
            return Rule.Start(PowOperand)
                       .Let(
                            Rule.StartIgnoringText(Token.CARET)
                                .Require(PowOperands)
                                .Return(tail => Node.MakeOperatorAndRightOperand(Tag.Power, tail))
                        )
                       .Return((left, operatorAndRight) => Node.MakeRightAssociativeExpression(left, operatorAndRight));
        }

        private static NodeRule PowOperand()
        {
            return Rule.Use(Terminal);
        }

        private static NodeRule Terminal()
        {
            return Rule.Use(Token.INTEGER, Tag.Integer)
                 | Rule.Use(Token.REAL, Tag.Real)
                 | Rule.Use(Token.STRING, Tag.String)
                 | Rule.StartIgnoringText(Token.LPAREN)
                       .Require(Expression, ErrorMessages.MissingExpression)
                       .RequireIgnoringText(Token.RPAREN)
                       .Return();
        }

        private static NodeRule LValues()
        {
            return Rule.Start(LValue)
                       .Let(
                            Rule.StartIgnoringText(Token.COMMA)
                                .Require(LValues)
                                .Return()
                        )
                       .Return((lvalue, lvalues) => Node.Cons(Tag.LValue, lvalue, lvalues));
        }

        private static NodeRule LValue()
        {
            return Rule.Start(Token.IDENTIFIER)
                       .Let(ArraySuffix)
                       .Return((identifier, array) => new Node(Tag.Identifier, identifier.ToUpper(), array));
        }

        private static NodeRule ArraySuffix()
        {
            return Rule.StartIgnoringText(Token.LBRACKET)
                       .Require(Expressions, ErrorMessages.MissingExpression)
                       .RequireIgnoringText(Token.RBRACKET)
                       .Return();
        }
    }
}
