module Tests

open Program
open NUnit.Framework
open FsUnit

type Tests () =
    static member TestCases = [|
        Add(Number 42, Multiply(Number 2, Number 54)), 150
        Number 1, 1
        Number 0, 0
        Subtraction(Division(Number 100, Number 3), Number 33), 0
    |]
        
    [<SetUp>]
    member this.Setup () =
        ()
        
    [<TestCaseSource(nameof(Tests.TestCases))>]
    [<Test>]
    member this.``Evaluation should work correct`` (testCase) =
        let expression, result = testCase
        expression |> evaluate |> should equal result