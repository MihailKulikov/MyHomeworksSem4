module LambdaInterpreterTests

open System
open FsCheck
open NUnit.Framework
open Program
open FsUnit

type Properties =
    static member ``getting new variable not belonging to set should be correct`` set =
        Set.contains (getNewVariableNotBelongingTo set) set |> not

type Tests () =
    static member GetSetOfFreeVariablesOfTestCases =
        let x = Guid.NewGuid()
        let y = Guid.NewGuid()
        let z = Guid.NewGuid()
        [|
            Application (Abstraction (x, Abstraction (y, Variable x)),
                         Abstraction(x, Application(Variable z, Variable x))),
            Set.empty.Add z
            Variable x,  Set.empty.Add x
            Application (Variable x, Variable y), Set.empty |> Set.add x |> Set.add y
            Abstraction (x, Variable x), Set.empty
        |]
        
    static member SubstitutionTestCases =
        let x = Guid.NewGuid()
        let y = Guid.NewGuid()
        let z = Guid.NewGuid()
        [|
            x, Application (Variable y, Variable z), Variable x, Application (Variable y, Variable z)
            x, Application (Variable y, Variable z), Variable y, Variable y
            x, Variable z, Application (Variable x, Variable y), Application (Variable z, Variable y)
            x, Variable z, Abstraction (x, Variable y), Abstraction (x, Variable y)
            x, Variable z, Abstraction (y, Variable x), Abstraction (y, Variable z)
        |]
        
    static member NormalReductionTestCases =
        let x = Guid.NewGuid()
        let y = Guid.NewGuid()
        [|
            Application (Abstraction (x, Variable y),
                         Application (Abstraction (x, Application(Application(Variable x, Variable x), Variable x)),
                                      Abstraction (x, Application(Application(Variable x, Variable x), Variable x)))),
            Variable y
            
            Application (Abstraction (x, Variable x), Abstraction (x, Variable x)),
            Abstraction(x, Variable x)
            
            Application (Abstraction (x, Abstraction (y, Variable x)), Abstraction (x, Variable x)),
            Abstraction (y, Abstraction (x, Variable x))
        |]
    
    [<TestCaseSource(nameof Tests.GetSetOfFreeVariablesOfTestCases)>]
    member this.``Should get set of free variables of lambda term correctly`` (testCase) =
        let term, expectedSet = testCase
        getSetOfFreeVariablesOf term |> Set.toList |> should equivalent <| Set.toList expectedSet
       
    [<TestCaseSource(nameof Tests.SubstitutionTestCases)>]
    member this.``Should substitute correctly`` (testCase) =
        let variableThatChanges, substitutedTerm, initialTerm, expectedResult = testCase
        substitute variableThatChanges substitutedTerm initialTerm |> should equal expectedResult
        
    [<TestCaseSource(nameof Tests.NormalReductionTestCases)>]
    member this.``Terms with normal form must be reduced to it by normal reduction `` (testCase) =
        let term, expectedNormalForm = testCase
        applyNormalReduction term |> should equal expectedNormalForm
       
    [<Test>]
    member this.``Random tests`` () =
        Check.QuickThrowOnFailureAll<Properties>()