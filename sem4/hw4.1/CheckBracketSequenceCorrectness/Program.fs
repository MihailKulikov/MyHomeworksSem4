module Program

/// Checks the correctness of a sequence of brackets of the specified types.
let check (bracketSequence: string) (bracketPairs: (char * char) list) =
    let leftToRightMap = Map.ofList bracketPairs
    let rightToLeftMap = bracketPairs |> List.map (fun (left, right) -> right, left) |> Map.ofList
    let rec loop stack (bracketSequence: string) =
        match stack with
        | [] ->
            if bracketSequence.Length = 0 then true
            else
                if Map.containsKey bracketSequence.[0] leftToRightMap then
                    loop [bracketSequence.[0]] bracketSequence.[1..]
                else
                    false
        | head :: tail ->
            if bracketSequence.Length = 0 then false
            else
                if Map.containsKey bracketSequence.[0] leftToRightMap then
                    loop (bracketSequence.[0] :: head :: tail) bracketSequence.[1..]
                else
                    if rightToLeftMap.[bracketSequence.[0]] <> head then false
                    else loop tail bracketSequence.[1..]
    let getKeys = Map.toSeq >> Seq.map fst >> Set.ofSeq
    let intersection = leftToRightMap |> getKeys |> Set.intersect (getKeys rightToLeftMap)
    let union = leftToRightMap |> getKeys |> Set.union (getKeys rightToLeftMap)
    if Set.count intersection = 0 && Seq.exists (fun char -> Set.contains char union |> not) bracketSequence then
        None
    else
        loop [] bracketSequence |> Some

[<EntryPoint>]
let main _ =
    match check "((())){}" ['(', ')'; '}', '{' ] with
    | Some(result) -> printfn $"%A{result}"
    | None -> printfn "Something goes wrong."
    0