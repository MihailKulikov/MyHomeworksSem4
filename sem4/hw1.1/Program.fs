open System

let rec factorial (n: UInt64): UInt64=
    match n with
    | 0UL -> 1UL
    | _ -> n * factorial (n - 1UL)
    
[<EntryPoint>]
let main _ =
    match Console.ReadLine() |> UInt64.TryParse with
    | true, n -> printf $"{factorial n}"
    | _ -> printf "The input must be a non-negative integer."
    0