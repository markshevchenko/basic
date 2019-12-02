BASIC
=====

BASIC is the simple implementation that designed to demonstrate such
techniques as recursive descent parser and threaded code.

It is designed for .NET Core and uses the expression trees and the dynamic
type variables.

It also demonstrates **dependency injection** and **unit testing**.

The solution is refactored to contain single project.

## Quick Start

Run **basic.exe** to start BASIC. You'll see the prompt:

```
basic 1.0.0.0.
_
```

Type `quit` end press <kbd>Enter</kbd> to exit.

Use *sqare brackets* with arrays:

```
_ dim a[10]
A[10] created.
_ for i = 1 to 10 a[i] = i next i
10
_ for i = 1 to 10 print a[i] next i
1
2
3
4
5
6
7
8
9
10
```

## Grammar

```
start-rule	: INTEGER ('NEXT' | statement)
		| statement
		;

statement	: 'DIM' IDENTIFIER array
		| 'RANDOMIZE' expression
		| 'PRINT' expressions ';'?
		| 'INPUT' (STRING ',')? l-value
		| 'IF' condition 'THEN' statement ('ELSE' statement)?
		| 'FOR' l-value '=' expression 'TO' expression ('STEP' expression)? (statement 'NEXT')?
		| 'GOTO' expression
		| 'REMOVE' range
		| 'LIST' range?
		| 'SAVE' STRING?
		| 'LOAD' STRING
		| 'REM' COMMENT
		| 'RUN'
		| 'END'
		| 'QUIT'
		| 'LET'? assignment
		;

assignment	: l-value '=' expression
		;

range		: INTEGER ('-' INTEGER)?
		;

condition	: or-operands
		;

or-operands	: or-operand ('OR' | 'XOR' or-operands)?
		;

or-operand	: and-operands
		;

and-operands	: and-operand ('AND' and-operands)?
		;

and-operand	: not-operand
		;

not-operand	: 'NOT' not-operand
		| '(' condition ');
		| relation
		;

relation	: expression '=' expression
		| expression '<>' expression
		| expression '<' expression
		| expression '>' expression
		| expression '<=' expression
		| expression '>=' expression
		;

expressions	: expression (',' expressions)?
		;

expression	: add-operands
		;

add-operands	: add-operand ('+' | '-' add-operands)?
		;

add-operand	: mul-operands
		;

mul-operands	: mul-operand ('*' | '/' | 'MOD' mul-operands)?
		;

mul-operand	: un-operand
		;

un-operand	: value ('^' pow-operands)?

pow-operands	: pow-operand ('^' pow-operands)?
		;


pow-operand	: '-' mul-operand
		| '+' mul-operand
		| value
		;

value		: constant
		| l-value
		| function '(' expressions? ')'
		| '(' expression ')'
		;

l-value		: IDENTIFIER array-suffix?
				;

array-suffix	: '[' expressions ']'
		;

constant	: INTEGER
		| FLOAT
		| STRING
		;

function	: 'RND'
		| 'LEN'
		| 'STR'
		| 'CHR'
		| 'ASC'
		| 'MID'
		| 'UPPER'
		| 'LOWER'
		| 'TRIM'
		| 'INSTR'
		| 'INSTRREV'
		| 'REPLACE'
		| 'JOIN'
		| 'SPLIT'
		| 'ABS'
		| 'SIGN'
		| 'MAX'
		| 'MIN'
		| 'EXP'
		| 'LN'
		| 'LOG'
		| 'SQRT'
		| 'SIN'
		| 'COS'
		| 'TAN'
		| 'ASIN'
		| 'ACOS'
		| 'ATAN'
		| 'ATAN2'
		| 'CEIL'
		| 'FLOOR'
		;
```