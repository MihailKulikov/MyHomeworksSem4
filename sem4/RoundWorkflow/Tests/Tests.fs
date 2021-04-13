module Tests

open NUnit.Framework
open FsUnit
open Program

let rounding digits = RoundBuilder digits

[<Test>]
let TestFromTaskDescription () =
    rounding 3 {
        let! a = 2.0 / 12.0
        let! b = 3.5
        return a / b
    } |> should equal 0.048
    
[<Test>]
let ``Must round at each calculation step`` () =
    rounding 1 {
        let! a = 1.0 / 3.0
        let! b = 3.0
        return a * b
    } |> should equal 0.9