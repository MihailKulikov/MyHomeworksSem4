module Program

let combinations size set =
    let rec loop acc size set = seq {
      match size, set with
      | n, x::xs ->
          if n > 0 then yield! loop (x::acc) (n - 1) xs
          if n >= 0 then yield! loop acc n xs
      | 0, [] -> yield acc
      | _, [] -> () }
    loop [] size set
    
let isPalindrome number =
    let stringRepresentation = number.ToString().ToCharArray()
    stringRepresentation = Array.rev stringRepresentation
    
let findMaxPalindrome() =
    let combinationsWithRepetition = Seq.init 900 (fun i -> [i + 100; i + 100])
                                     |> Seq.append <| combinations 2 [100..999]
    combinationsWithRepetition |> Seq.map (fun pair -> pair.[0] * pair.[1]) |> Seq.filter isPalindrome |> Seq.max

[<EntryPoint>]
let main _ =
    printf $"%A{findMaxPalindrome()}"
    0