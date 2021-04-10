namespace LocalNetwork

open System.Collections.Generic

/// Represents the local network.
type LocalNetwork(computers: Computer list, adjacencyMap: IDictionary<Computer, Computer list>) =
    let canThisComputerBeInfected (computer: Computer) =
        computer.IsInfected = false && computer.ProbabilityOfInfection > 0.0

    let canThisComputerInfect (computer: Computer) =
        computer.IsInfected = true
        && List.exists canThisComputerBeInfected adjacencyMap.[computer]
        
    let tryInfectNeighboursOf (computer: Computer) =
        List.iter (fun (neighbour: Computer) -> neighbour.TryToGetInfected()) adjacencyMap.[computer]
    
    /// Returns true if the state of the local network can change; false otherwise.
    member this.IsEndOfSimulation () = List.exists canThisComputerInfect computers

    /// Makes step in the simulation.
    member this.MakeStep () =
        List.iter tryInfectNeighboursOf computers
        List.map (fun (computer: Computer) -> computer.IsInfected) computers