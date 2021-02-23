module Program

/// <summary>
/// Represents the abstract syntax tree of an expression in a simple programming language that supports addition,
/// subtraction, multiplication and division of numbers and variables.
/// </summary>
type Expression =
    | Number of int
    | Add of Expression * Expression
    | Subtraction of Expression * Expression
    | Multiply of Expression * Expression
    | Division of Expression * Expression

/// <summary>Recursively process the syntax tree.</summary>
/// <param name="expression"> The input abstract syntax tree of an expression.</param>
/// <returns>Result of evaluated expression.</returns>
let rec evaluate expression =
    match expression with
    | Number n -> n
    | Add (x, y) -> evaluate x + evaluate y
    | Subtraction (x, y) -> evaluate x - evaluate y
    | Multiply (x, y) -> evaluate x * evaluate y
    | Division (x, y) -> evaluate x / evaluate y

[<EntryPoint>]
let main _ =
    0