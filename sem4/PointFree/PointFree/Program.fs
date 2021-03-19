let pointFul x l =
    List.map (fun y -> y * x) l
    
let pointFree (x: int): int list -> int list = 
    List.map ((*))

[<EntryPoint>]
let main argv =
    0