module CalculateFlowTests

open NUnit.Framework
open Program
open FsUnit

[<SetUp>]
let Setup () =
    ()

[<Test>]
let ``Returns result with correct strings`` () =
    let calculate = CalculateBuilder()
    calculate {
        let! x = "1"
        let! y = "2"
        let z = x + y
        return z
    } |> should equal <| Some 3

[<Test>]
let ``Returns none with incorrect string`` () =
    let calculate = CalculateBuilder()
    calculate {
        let! x = "1"
        let! y = "Ъ"
        let z = x + y
        return z
    } |> should equal <| None
