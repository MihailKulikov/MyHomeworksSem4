module Tests

open NUnit.Framework
open Program
open FsUnit

type Tests () =
    static member CorrectSequencesTestCases = [|
        "", ['(', ')']
        "()", ['(', ')']
        ")(", [')', '(']
        "a[{}]b", ['a', 'b'; '[', ']'; '{', '}']
        "((())){[]}()", ['(', ')'; '{', '}'; '[', ']']
    |]
    
    static member IncorrectSequencesTestCases = [|
        "(", ['(', ')']
        ")", ['(', ')']
        "()", [')', '(']
        "a[{}]b[", ['a', 'b'; '[', ']'; '{', '}']
        "((()))[{[]}()", ['(', ')'; '{', '}'; '[', ']']
    |]
    
    static member IncorrectInputTestCases = [|
        "({", ['(', ')']
        ")}", ['(', ')']
        "([])", [')', '(']
        "a[{}]b[)", ['a', 'b'; '[', ']'; '{', '}']
        "a((()))[{[]}()", ['(', ')'; '{', '}'; '[', ']']
    |]
    
    [<TestCaseSource(nameof Tests.CorrectSequencesTestCases)>]
    [<Test>]
    member this.``Should return true then sequence is correct`` testCase =
        let sequence, pairs = testCase
        (check sequence pairs) |> should equal (Some true)
        
    [<TestCaseSource(nameof Tests.IncorrectSequencesTestCases)>]
    [<Test>]
    member this.``Should return false then sequence is not correct`` testCase =
        let sequence, pairs = testCase
        (check sequence pairs) |> should equal (Some false)
        
    [<TestCaseSource(nameof Tests.IncorrectInputTestCases)>]
    [<Test>]
    member this.``Should return None then input is incorrect`` testCase =
        let sequence, pairs = testCase
        (check sequence pairs) |> should equal None