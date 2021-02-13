open System
open System.Numerics

let factorial (n: BigInteger): BigInteger =
    if n < BigInteger.Zero then
        raise (ArgumentException "The input must be a non-negative integer")
    let rec loop (n: BigInteger) (acc: BigInteger): BigInteger =
        if n = BigInteger.Zero then
            acc
        else
            loop (acc * n) (n - BigInteger.One)
    loop n BigInteger.One
    
let tryFactorial (n: BigInteger) =
    try
        Some(factorial n)
    with
    | :? ArgumentException ->  None
    
[<EntryPoint>]
let main _ =
    match Console.ReadLine() |> BigInteger.TryParse with
    | true, n -> match tryFactorial n with
        | Some(result) -> printf $"{result}"
        | None -> printf "The input must be a non-negative integer."
    | _ -> printf "The input must be a non-negative integer."
    0