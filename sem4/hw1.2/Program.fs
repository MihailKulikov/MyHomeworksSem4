open System
open System.Numerics

let fibonacciNumbers =
    seq {
        yield! seq { BigInteger.Zero; BigInteger.One }
        yield! (BigInteger.Zero, BigInteger.One) |> Seq.unfold (fun state ->
            Some(fst state + snd state, (snd state, fst state + snd state)))}

let getNthFibonacciNumber n =
    if n <= 0 then None
    else Seq.item (n - 1) fibonacciNumbers |> Some

[<EntryPoint>]
let main _ =
    printf "Enter the number of the desired fibonacci number.\n"
    match Console.ReadLine() |> Int32.TryParse with
    | true, n ->
        match getNthFibonacciNumber n with
        | Some(result) -> printf "%A" result
        | None -> printf "The input must be a positive integer."
    | _ -> printf "The input must be a positive integer."
    0