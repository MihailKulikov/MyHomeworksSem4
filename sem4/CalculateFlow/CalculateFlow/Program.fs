module Program

open System  

type CalculateBuilder() =
    member this.Bind(x, f) =
        match Int32.TryParse(x: string) with
        | true, number -> f number
        | _ -> None
    member this.Return(x) =
        Some x

[<EntryPoint>]
let main _ =
    let calculate = CalculateBuilder()
    let result = calculate {
        let! x = "1"
        let! y = "3"
        let z = x + y
        return z
    }
    printf $"%A{result}"
    0