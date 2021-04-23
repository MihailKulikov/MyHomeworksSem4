namespace LazyRealisation

type LazyFactory =
    static member CreateSingleThreadedLazy (supplier: unit -> 'a) =
        let mutable isValueCreated = false
        let mutable isSupplierStarted = false
        let mutable value = null
        {
            new ILazy<'a> with
                member this.Get () =
                    if isValueCreated then value
                    else
                        if isSupplierStarted then invalidOp "Recursive calls Get()."
                        else
                            isSupplierStarted <- true
                            value <- supplier()
                            isValueCreated <- true
                            value
        }
    static member CreateMultithreadedLazy supplier =
        0
    static member CreateLockFreeLazy supplier =
        0