module Tests

open NUnit.Framework
open FsUnit
open Program

type Tests () =
    static member TestCaseList = [|
        Node(42, Node(1, Empty, Empty), Node(3, Empty, Empty)),
        (fun (x: int) -> x.ToString()),
        Node("42", Node("1", Empty, Empty), Node("3", Empty, Empty))
        
        Empty,
        (fun (x: int) -> x.ToString()),
        Empty
        
        Node(42, Node(1, Empty, Empty), Node(3, Empty, Empty)),
        (fun (x: int) -> (x + 5).ToString()),
        Node("47", Node("6", Empty, Empty), Node("8", Empty, Empty))
    |]
    
    [<SetUp>]
    member this.Setup () =
        ()
        
    [<TestCaseSource(nameof(Tests.TestCaseList))>]
    [<Test>]
    member this.``Should map correctly`` (testCase) =
        let input, mapFunction, result = testCase
        input |> map mapFunction |> should equal result