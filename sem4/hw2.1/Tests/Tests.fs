module Tests

open NUnit.Framework
open FsUnit
open FsCheck
open Program
open System

type ListProperties =
    static member ``filtering implementation should be equivalent to folding realisation`` list =
            (evenNumberCountFiltering list) = (evenNumberCountFolding list)
    static member ``filtering implementation should be equivalent to mapping realisation`` list =
            (evenNumberCountFiltering list) = (evenNumberCountMapping list)
    static member ``folding implementation should be equivalent to mapping realisation`` list =
            (evenNumberCountFolding list) = (evenNumberCountMapping list)

[<TestFixture("Map")>]
[<TestFixture("Fold")>]
[<TestFixture("Filter")>]
type Tests (realisation) =
    member this.evenCount =
        match realisation with
        | "Map" -> evenNumberCountMapping
        | "Fold" -> evenNumberCountFolding
        | "Filter" -> evenNumberCountFiltering
        | _ -> raise (ArgumentException("Invalid test fixture parameter."))
        
    static member TestCaseList = [|
        [], 0;
        [42; 54], 2 
        [-2; -1], 1;
        [0; -0], 2;
        [1; 100], 1
        [1000000; 1000000001], 1
        [Int32.MaxValue; Int32.MinValue], 1;
    |]
 
    [<Test>]
    member this.``Functions should be equivalent`` () =
        Check.QuickThrowOnFailureAll<ListProperties>()
    
    [<TestCaseSource(nameof(Tests.TestCaseList))>]
    [<Test>]
    member this.``Mapping realisation should work on various inputs`` testCase =
        let data, expectedResult = testCase
        this.evenCount data |> should equal expectedResult