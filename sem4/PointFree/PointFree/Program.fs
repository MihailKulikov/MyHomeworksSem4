module Program

let pointFul x l =
    List.map (fun y -> y * x) l
    
let pointFul' x =
    List.map (fun y -> y * x)
    
let pointFul'' x =
    List.map ((*) x)

let pointFree = 
    (*) >> List.map

[<EntryPoint>]
let main _ =
    0