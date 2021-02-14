open System.Numerics

let fibonacciNumbers =
    (BigInteger.Zero, BigInteger.One)
    |> Seq.unfold (fun state ->
        Some(fst state + snd state, (snd state, fst state + snd state)))


[<EntryPoint>]
let main _ =
    Seq.iter (printf "%A ") fibonacciNumbers
    0