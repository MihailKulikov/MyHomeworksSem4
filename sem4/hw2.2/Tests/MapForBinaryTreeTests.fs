module Tests

open NUnit.Framework
open FsUnit
open Program

type Tests () =
    static member TestCaseList = [|
        Node(42, Node(1, Leaf, Leaf), Node(3, Leaf, Leaf)),
        (fun (x: int) -> x.ToString()),
        Node("42", Node("1", Leaf, Leaf), Node("3", Leaf, Leaf));
        
        Leaf,
        (fun (x: int) -> x.ToString()),
        Leaf
        
        Node(42, Node(1, Leaf, Leaf), Node(3, Leaf, Leaf)),
        (fun (x: int) -> (x + 5).ToString()),
        Node("47", Node("6", Leaf, Leaf), Node("8", Leaf, Leaf));
    |]
    
    [<SetUp>]
    member this.Setup () =
        ()
        
    [<TestCaseSource(nameof(Tests.TestCaseList))>]
    [<Test>]
    member this.``Should map correctly`` (testCase) =
        let input, mapFunction, result = testCase
        input |> map mapFunction |> should equal result