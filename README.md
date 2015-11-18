Learning Interpreter
====================

Learning Interpreter is a simple BASIC implementation that designed to
demonstrate such techniques as recursive descent parser and threaded code.

It's developed for .NET framework and uses expression trees and dynamic
type variables.

It also demonstrates **dependency injection** and **unit testing**.

The solution contains four projects:

`LearningInterpreter`&ndash;&mdash; is the domain module implementing
`RunEvaluatePrintLoop` and `RunTimeEnvironment` with `Variables` and `Program`.
It implements general core for the any program language with the line numbers.

`LearningInterpreter.Console`&ndash;&mdash; implements console input and output,
and saving/loading to/from disk files.

Also, the project is the **composition root** of the entire solution, so it has
the entry point, assembles all together, and starts.

`LearningInterpreter.Basic`&ndash;&mdash; implement BASIC specific classes
like `BasicParser`, or `Print`.

`LearningInterpreter.Tests`&ndash;&mdash; implements about 500 unit tests.
