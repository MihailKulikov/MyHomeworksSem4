module Program

/// Checks the correctness of a sequence of brackets of the specified types.
let check (bracketSequence: string) bracketsPairs =
    let containsElement pair bracket =
        fst pair = bracket || snd pair = bracket
    let bracketToPair bracket =
        bracketsPairs |> List.find (fun pair -> containsElement pair bracket) 
    let isLeft bracket =
        (bracket |> bracketToPair |> fst) = bracket 
    let rec loop (bracketSequence: string) bracketPairToCountMap =
        if bracketSequence.Length = 0
        then
            bracketPairToCountMap
        else
            let currentBracket = bracketSequence.[0]
            let diff = if currentBracket |> isLeft then 1 else -1
            bracketPairToCountMap
            |> Map.change (bracketToPair currentBracket) (fun oldValue ->
                match oldValue with
                | Some(count) when count >= 0 -> Some (count + diff)
                | Some(count) -> Some (count)
                | None -> None)
            |> loop bracketSequence.[1..]
    if bracketSequence
       |> Seq.forall (fun bracket -> bracketsPairs |> List.exists (fun pair -> containsElement pair bracket)) |> not
    then
        None
    else
        bracketsPairs
        |> List.map (fun pair -> (pair, 0))
        |> Map.ofList
        |> loop bracketSequence
        |> Map.forall (fun _ count -> count = 0)
        |> Some

[<EntryPoint>]
let main _ =
    match check "((())){}" ['(', ')'; '}', '{' ] with
    | Some(result) -> printfn "%A" result
    | None -> printfn "Something goes wrong."
    0
