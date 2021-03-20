module Tests

open NUnit.Framework
open FsCheck
open Program

type Properties =
    static member ``initial to first`` x l =
        pointFul x l = pointFul' x l
    static member ``first to second`` x l =
        pointFul' x l = pointFul'' x l
    static member ``second to point free`` x l =
        pointFul'' x l = pointFree x l

[<Test>]
let CheckProperties () =
    Check.QuickThrowOnFailureAll<Properties> ()