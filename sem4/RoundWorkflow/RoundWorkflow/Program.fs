module Program

open System

/// Represents builder for rounding flow.
/// All calculations in it are rounded to the specified number of decimal places.
type RoundBuilder(digits: int) =
    member this.Bind(x: double, f) =
        Math.Round(x, digits) |> f
    member this.Return(x: double) =
        Math.Round(x, digits)

[<EntryPoint>]
let main _ =
    let rounding digits = RoundBuilder digits
    rounding 1 {
        let! a = 1.0 / 3.0
        let! b = 3.0
        return a * b
    } |> printf "%A"
    0