module PriorityQueue  

/// <summary>
/// Represents priority queue with enqueue and dequeue methods
/// </summary>
type PriorityQueue<'a> (capacity, comparator) =
    let pq : 'a array = Array.zeroCreate (capacity + 1)
    let mutable count = 0
    let isLess i j = comparator pq.[i] pq.[j] < 0

    let exchange i j =
        let temp = pq.[i]
        pq.[i] <- pq.[j]
        pq.[j] <- temp
        i
        
    let rec swim k =
        match (k > 1 && isLess (k/2) k) with
        | true -> exchange (k/2) k |> swim 
        | false -> ()

    let rec sink k =  
        match 2*k <= count, 2*k with
        | true, j when j < count && isLess j (j + 1) ->
            match isLess k (j+1), j+1 with  
            | true, x ->
                exchange k x |> ignore
                sink x
            | false, _ -> ()                                     
        | true, j ->
            exchange k j |> ignore
            sink j
        | false, _ -> ()  

    /// <summary>
    /// Checks if the priority queue is empty.
    /// </summary>
    /// <returns>True if the priority queue is empty, and false if not.</returns>
    member x.IsEmpty with get() = count = 0
    
    /// <summary>
    /// Gets the number of elements actually contained in the priority queue.
    /// </summary>
    member x.Length with get() = count

    /// <summary>
    /// Adds an object to the end of the Queue&lt;T&gt;.
    /// </summary>
    /// <param name="item">The object to add to the priority queue.</param>
    member x.Enqueue (item:'a) =
        count <- count + 1
        pq.[count] <- item
        swim count

    /// <summary>
    /// Removes and returns the object at the beginning of the priority queue.
    /// </summary>
    /// <returns>The object that is removed from the beginning of the priority queue.</returns>
    member x.Dequeue() =
        if count = 0 then invalidOp "Queue is empty."
        let max = pq.[1]
        exchange 1 count |> ignore
        count <- count - 1
        pq.[count+1] <- Unchecked.defaultof<'a>
        sink 1
        max
        
let main _ =
    0