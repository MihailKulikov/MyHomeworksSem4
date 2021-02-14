open System
open System.Numerics

let factorial n =
    let rec loop n acc =
        if n = BigInteger.Zero then acc
        else loop (n - BigInteger.One) (acc * n)
    if n < BigInteger.Zero then None
    else Some (loop n BigInteger.One)
    
[<EntryPoint>]
let main _ =
    printf "Enter the number for which you want to calculate the factorial. \n"
    match Console.ReadLine() |> BigInteger.TryParse with
    | true, n ->
        match factorial n with
        | Some(result) -> printf $"{result}"
        | None -> printf "The input must be a non-negative integer."
    | _ -> printf "The input must be a non-negative integer."
    0