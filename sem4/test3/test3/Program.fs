module PriorityQueue  

type PriorityQueue<'a> (capacity, comparator) =
    let pq : 'a array = Array.zeroCreate (capacity + 1)
    let mutable count = 0
    let Less i j = comparator pq.[i] pq.[j] < 0

    let Exchange i j =
        let temp = pq.[i]
        pq.[i] <- pq.[j]
        pq.[j] <- temp
        i
        
    let rec Swim k =
        match (k > 1 && Less (k/2) k) with
        | true -> Exchange (k/2) k |> Swim 
        | false -> ()

    let rec Sink k =  
        match 2*k <= count, 2*k with
        | true, j when j < count && Less j (j + 1) ->
            match Less k (j+1), j+1 with  
            | true, x ->
                Exchange k x |> ignore
                Sink x
            | false, _ -> ()                                     
        | true, j ->
            Exchange k j |> ignore
            Sink j
        | false, _ -> ()  

    member x.IsEmpty with get() = count = 0
    member x.Size with get() = count

    member x.Insert (item:'a) =
        count <- count + 1
        pq.[count] <- item
        Swim count

    member x.Pop() =
        if count = 0 then invalidOp "Queue is empty."
        let max = pq.[1]
        Exchange 1 count |> ignore
        count <- count - 1
        pq.[count+1] <- Unchecked.defaultof<'a>
        Sink 1
        max
        
let main _ =
    0