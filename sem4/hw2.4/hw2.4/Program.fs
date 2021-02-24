module Program

let isPrime (number: bigint) =
    if number <= bigint.One then false
    else
        let upperBound = number |> float |> sqrt |> bigint
        seq { bigint(2)..upperBound }
        |> Seq.exists(fun x -> number % x = bigint.Zero)
        |> not
    
let primes =
    Seq.initInfinite(fun x -> bigint(x) + bigint(2))
    |> Seq.filter(isPrime)

[<EntryPoint>]
let main _ =
    0