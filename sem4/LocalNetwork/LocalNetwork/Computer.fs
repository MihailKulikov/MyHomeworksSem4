namespace LocalNetwork

/// Possible types of operating systems in the local network. 
type Os =
    | Windows = 0
    | Linux = 1
    | MacOs = 2

/// Represents computer in the local network.
type Computer =
    /// Tries to get infected.
    abstract member TryToGetInfected: unit -> unit
    
    /// Returns true, if this computer is infected; false, otherwise.
    abstract member IsInfected: bool
    
    /// Probability of getting infected.
    abstract member ProbabilityOfInfection: float