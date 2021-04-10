module LocalNetworkTests

open System.Collections.Generic
open LocalNetwork
open NUnit.Framework
open FsUnit

let getProbabilityFor os = function
    | Os.Windows -> 0.3
    | Os.Linux -> 0.1
    | Os.MacOs -> 0.4
    | _ -> 0.0
    
let getVulnerableComputer () =
    { new Computer with
        member this.ProbabilityOfInfection = 1.0
        member this.TryToGetInfected () = ()
        member this.IsInfected = false }
let getInfectedComputer () =
    { new Computer with
        member this.ProbabilityOfInfection = 0.0
        member this.TryToGetInfected () = ()
        member this.IsInfected = true }

[<Test>]
let ``Simulation should work like BFS if all probabilities equal to one.`` () =
    let infectedComputer = getInfectedComputer()
    let firstLayerOfVulnerableComputers = List.init 2 (fun _ -> getVulnerableComputer())
    let secondLayerOfVulnerableComputers = List.init 4 (fun _ -> getVulnerableComputer())
    let adjacencyMap = Dictionary<Computer, Computer list>()
    adjacencyMap.Add (infectedComputer, firstLayerOfVulnerableComputers)
    adjacencyMap.Add (firstLayerOfVulnerableComputers.[0], secondLayerOfVulnerableComputers.[0..1])
    adjacencyMap.Add (firstLayerOfVulnerableComputers.[1], secondLayerOfVulnerableComputers.[2..])
    let allComputers = seq { infectedComputer :: firstLayerOfVulnerableComputers; secondLayerOfVulnerableComputers}
                       |> List.concat
    let localNetwork = LocalNetwork(allComputers, adjacencyMap)
    
    localNetwork.IsEndOfSimulation |> should be False
    localNetwork.MakeStep |> should equal [True, True, True, False, False, False, False]
    localNetwork.IsEndOfSimulation |> should be False
    localNetwork.MakeStep |> should equal [for _ in 1..7 -> True]
    localNetwork.IsEndOfSimulation |> should be True
    
    
    