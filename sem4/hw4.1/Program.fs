open System.Numerics
open System

let powersOfTwo n m =
    let two = BigInteger.One + BigInteger.One
    let rec loop element index =
        if index = m + 1 then [] else element :: loop (element * two) (index + 1)
    loop (BigInteger.Pow(two, n)) 0
    
[<EntryPoint>]
let main _ =
    printf "enter numbers n and m separated by a space.\n"
    let input = Console.ReadLine().Split(' ') |> Seq.toList |> List.map Int32.Parse
    powersOfTwo (List.head input) (List.last input) |> printf "%A"
    0