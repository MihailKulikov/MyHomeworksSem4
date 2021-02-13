open System

let find list item =
    let rec loop list index =
        match list with
        | [] -> None
        | h :: t when h <> item -> loop t (index + 1)
        | _ -> Some(index)
    loop list 0
    
[<EntryPoint>]
let main _ =
    printf "Enter a list separated by a space. \n"
    let list = Console.ReadLine().Split " " |> Seq.toList |> List.map Int32.Parse
    printf "Enter a item for search. \n"
    match Console.ReadLine() |> Int32.Parse |> find list with
    | Some(index) -> printf "%d" index
    | None -> printf "There is no such item."
    0