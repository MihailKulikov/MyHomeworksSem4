module Program

open System.Numerics

let max = BigInteger(4000000)

let fibonacciNumbersSmallerThan threshold =
    (BigInteger.Zero, BigInteger.One)
    |> Seq.unfold (fun state ->
        if fst state + snd state < threshold then
            Some(fst state + snd state, (snd state, fst state + snd state))
        else None)

let sumOfEvenFibonacciNumbersSmallerThan threshold =
    let fibSeq = fibonacciNumbersSmallerThan threshold
    fibSeq |> Seq.filter (fun number -> number % BigInteger(2) = BigInteger.Zero) |> Seq.sum

[<EntryPoint>]
let main _ =
    printf $"%A{sumOfEvenFibonacciNumbersSmallerThan max}"
    0