namespace LocalNetwork

/// Possible types of operating systems in the local network. 
type Os =
    | Windows = 0
    | Linux = 1
    | MacOs = 2

/// Represents computer in the local network.
type Computer =
    abstract member TryToGetInfected: unit -> unit
    abstract member IsInfected: bool
    abstract member ProbabilityOfInfection: float