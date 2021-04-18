module LocalNetworkTests

open System.Collections.Generic
open LocalNetwork
open NUnit.Framework
open FsUnit
open Foq

let getOs (probabilityOfInfection: float) =
    Mock<Os>()
        .Setup(fun os -> <@ os.ProbabilityOfInfection @>)
        .Returns(probabilityOfInfection)
        .Create()

type VulnerableComputer() =
    let mutable isInfected = false
    interface Computer with
        member this.Os = getOs 1.0
        member this.TryToGetInfected() = isInfected <- true
        member this.IsInfected
            with get() = isInfected

        member this.CanThisComputerBeInfected = not isInfected

type ProtectedComputer() =
    let mutable isInfected = false
    interface Computer with
        member this.Os = getOs 0.0
        member this.TryToGetInfected () = ()
        member this.IsInfected
            with get() = isInfected

        member this.CanThisComputerBeInfected = false

let getInfectedComputer () =
    Mock<Computer>()
        .Setup(fun computer -> <@ computer.IsInfected @>)
        .Returns(true)
        .Setup(fun computer -> <@ computer.Os @>)
        .Returns(getOs(0.0))
        .Setup(fun computer -> <@ computer.TryToGetInfected() @>)
        .Returns(())
        .Create()
    
                               

[<Test>]
let ``Simulation should work like BFS if all probabilities equal to one.`` () =
    let infectedComputer = getInfectedComputer ()
    let firstLayerOfVulnerableComputers = List.init 2 (fun _ -> VulnerableComputer() :> Computer)
    let secondLayerOfVulnerableComputers = List.init 4 (fun _ -> VulnerableComputer() :> Computer)
    let adjacencyMap = Dictionary<Computer, Computer list>()
    adjacencyMap.Add (infectedComputer, firstLayerOfVulnerableComputers)
    adjacencyMap.Add (firstLayerOfVulnerableComputers.[0], secondLayerOfVulnerableComputers.[0..1])
    adjacencyMap.Add (firstLayerOfVulnerableComputers.[1], secondLayerOfVulnerableComputers.[2..])
    secondLayerOfVulnerableComputers
    |> List.indexed
    |> List.iter (fun (i, computer) -> adjacencyMap.Add (computer, [firstLayerOfVulnerableComputers.[if i < 2 then 0 else 1]]))
    firstLayerOfVulnerableComputers
    |> List.iter (fun computer -> adjacencyMap.[computer] <- infectedComputer :: adjacencyMap.[computer])
    let allComputers = seq { infectedComputer :: firstLayerOfVulnerableComputers; secondLayerOfVulnerableComputers}
                       |> List.concat
    let localNetwork = LocalNetwork(allComputers, adjacencyMap)
    
    localNetwork.IsEndOfSimulation() |> should not' (be True)
    localNetwork.MakeStep() |> should equal [true; true; true; false; false; false; false]
    localNetwork.IsEndOfSimulation() |> should not' (be True)
    localNetwork.MakeStep() |> should equal [for _ in 1..7 -> true]
    localNetwork.IsEndOfSimulation() |> should be True

[<Test>]    
let ``Simulation should end if all probabilities equal to zero`` () =
    let infectedComputer = getInfectedComputer ()
    let adjacencyMap = Dictionary<Computer, Computer list>()
    let protectedComputers = List.init 3 (fun _ -> ProtectedComputer() :> Computer)
    adjacencyMap.Add (infectedComputer, protectedComputers)
    protectedComputers
    |> List.iter (fun computer -> adjacencyMap.Add (computer, [infectedComputer]))
    let allComputers = infectedComputer :: protectedComputers
    let localNetwork = LocalNetwork(allComputers, adjacencyMap)
    
    localNetwork.IsEndOfSimulation() |> should be True
    localNetwork.MakeStep() |> should equal [true; false; false; false]
    localNetwork.IsEndOfSimulation() |> should be True