module Program
    
    /// <summary>
    /// Counts the number of even numbers in a list using the map function.
    /// </summary>
    /// <param name="list">List in which you need to count the number of even numbers.</param>
    /// <returns>The number of even numbers in the list.</returns>
    let evenNumberCountMapping list =
        list
        |> List.map (fun x -> if x % 2 = 0 then 1 else 0) 
        |> List.sum
    
    /// <summary>
    /// Counts the number of even numbers in a list using the filter function.
    /// </summary>
    /// <param name="list">List in which you need to count the number of even numbers.</param>
    /// <returns>The number of even numbers in the list.</returns>
    let evenNumberCountFiltering list =
        list
        |> List.filter (fun x -> x % 2 = 0)
        |> List.length
    
    /// <summary>
    /// Counts the number of even numbers in a list using the fold function.
    /// </summary>
    /// <param name="list">List in which you need to count the number of even numbers.</param>
    /// <returns>The number of even numbers in the list.</returns>
    let evenNumberCountFolding list =
        list
        |> List.fold (fun acc x -> if x % 2 = 0 then acc + 1 else acc) 0
    
    [<EntryPoint>]
    let main _ =
        0