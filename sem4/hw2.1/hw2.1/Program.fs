module Program
    let evenNumberCountMapping list =
        list
        |> List.map(fun x -> if x % 2 = 0 then 1 else 0) 
        |> List.sum
    
    let evenNumberCountFiltering list =
        list
        |> List.filter(fun x -> if x % 2 = 0 then true else false)
        |> List.length
    
    let evenNumberCountFolding list =
        list
        |> List.fold (fun acc x -> if x % 2 = 0 then acc + 1 else acc) 0
    
    [<EntryPoint>]
    let main _ =
        0