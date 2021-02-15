open System.Numerics
open System

let powersOfTwo n m =
    if n < 0 || m < 0 then None else
    let two = BigInteger.One + BigInteger.One
    let startPow = BigInteger.Pow(two, n)
    let rec loop element acc =
        match acc with
        | h :: _ when h = startPow -> acc
        | _ -> loop (element / two) (element :: acc)
    loop (BigInteger.Pow(two, n + m)) [] |> Some
    
[<EntryPoint>]
let main _ =
    let parse (str: String) =
        match Int32.TryParse str with
        | true, number -> Some(number)
        | _ -> None
    printf "Enter numbers n and m separated by a space.\n"
    match Console.ReadLine().Split ' ' |> Seq.toList |> List.map parse with
    | [Some(head); Some(last)] ->
        match powersOfTwo head last with
        | Some(result) -> printf "%A" result
        | None -> printf "n and m should be non-negative integers."
    | _ -> printf "n and m should be non-negative integers."
    0