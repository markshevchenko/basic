Learning BASIC
==============

Learning BASIC is a simple interpretator that designed to demonstrate basic techniques of creataing interpreters
and compilers.

It's developed for .NET framework, so it uses expression tree and dynamic typing.

This program demonstrates also a few of techniques of design and development like **dependency injection** and **unit testing**.

The solution contains four projects:

`LearningBasic`&ndash;&mdash; implements domain classes `RunTimeEnvironment` and `ReadEvaluatePrintLoop`, declares interfaces for parsing and input/output.

`LearningBasic.Console`&ndash;&mdash; implements interfaces to console input and output. Also, this project is the **composition root** of the entire solution, so it assembles all together, and has the entry point.

`LearningBasic.Parsing`&ndash;&mdash; implement parsing interfaces for the read-evaluate-print loop and the run-time environtment. In short, this project can get a string a then return some BASIC statement to run.

`LearningBasic.Tests`&ndash;&mdash; implements about 500 unit tests for classes from other projects.
