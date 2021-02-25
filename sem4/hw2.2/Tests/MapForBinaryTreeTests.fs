module Tests

open NUnit.Framework
open FsUnit
open Program

type Tests () =
    static member TestCaseList = [|
        Node(42, Node(1, Empty, Empty), Node(3, Empty, Empty)),
        (fun (x: int) -> string x),
        Node("42", Node("1", Empty, Empty), Node("3", Empty, Empty))
        
        Empty,
        (fun (x: int) -> string x),
        Empty
        
        Node(42, Node(1, Empty, Empty), Node(3, Empty, Empty)),
        (fun (x: int) -> string (x + 5)),
        Node("47", Node("6", Empty, Empty), Node("8", Empty, Empty))
    |]
        
    [<TestCaseSource(nameof(Tests.TestCaseList))>]
    [<Test>]
    member this.``Should map correctly`` (testCase) =
        let input, mapFunction, result = testCase
        input |> map mapFunction |> should equal result