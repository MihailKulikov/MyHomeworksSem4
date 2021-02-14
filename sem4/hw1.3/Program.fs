open System

let reverse list =
    List.fold (fun acc el -> el :: acc) [] list

[<EntryPoint>]
let main _ =
    printf "Enter a list separated by a space. \n"
    Console.ReadLine().Split " " |> Seq.toList |> reverse |> printf "%A"
    0