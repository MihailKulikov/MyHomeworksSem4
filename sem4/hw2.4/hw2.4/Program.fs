module Program

/// <summary>Checks that the specified number is prime.</summary>
/// <param name="number">The specified number.</param>
/// <results>true if the specified number is prime; otherwise false.</results>
let isPrime (number: bigint) =
    if number <= bigint.One then false
    else
        let upperBound = number |> float |> sqrt |> bigint
        seq { bigint(2)..upperBound }
        |> Seq.exists(fun x -> number % x = bigint.Zero)
        |> not

/// Infinite sequence of prime numbers.
let primes =
    Seq.initInfinite(fun x -> bigint(x) + bigint(2))
    |> Seq.filter(isPrime)

[<EntryPoint>]
let main _ =
    0