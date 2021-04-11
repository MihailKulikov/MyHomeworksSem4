namespace LocalNetwork

open System.Collections.Generic

/// Represents the local network.
type LocalNetwork(computers: Computer list, adjacencyMap: IDictionary<Computer, Computer list>) =
    let canThisComputerBeInfected (computer: Computer) =
        not computer.IsInfected && computer.ProbabilityOfInfection > 0.0

    let canThisComputerInfect (computer: Computer) =
        computer.IsInfected && List.exists canThisComputerBeInfected adjacencyMap.[computer]
        
    let tryInfectNeighboursOf (computer: Computer) =
        List.iter (fun (neighbour: Computer) -> neighbour.TryToGetInfected()) adjacencyMap.[computer]
    
    /// Returns true if the state of the local network can change; false otherwise.
    member this.IsEndOfSimulation () = List.exists canThisComputerInfect computers |> not

    /// Makes step in the simulation.
    member this.MakeStep () =
        computers |> List.filter canThisComputerInfect |>  List.iter tryInfectNeighboursOf
        List.map (fun (computer: Computer) -> computer.IsInfected) computers