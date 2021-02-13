open System
open System.Numerics

let rec factorial (acc: BigInteger) (n: BigInteger): BigInteger =
    if n = BigInteger.Zero then
        acc
    else
        if n < BigInteger.Zero then
            raise (ArgumentException "The input must be a non-negative integer")
        else
            factorial (acc * n) (n - BigInteger.One)
    
let tryFactorial (n: BigInteger) =
    try
        Some(factorial BigInteger.One n)
    with
    | :? ArgumentException as e -> printf $"{e.Message}"; None
    
[<EntryPoint>]
let main _ =
    match Console.ReadLine() |> BigInteger.TryParse with
    | true, n -> match tryFactorial n with
        | Some(result) -> printf $"{result}"
        | None -> printf "The input must be a non-negative integer."
    | _ -> printf "The input must be a non-negative integer."
    0