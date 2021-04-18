namespace LocalNetwork

/// Possible types of operating systems in the local network. 
type Os =
    /// Probability of getting infected.
    abstract member ProbabilityOfInfection: float
    

/// Represents computer in the local network.
type Computer =
    /// Tries to get infected.
    abstract member TryToGetInfected: unit -> unit
    
    /// Returns true, if this computer is infected; false, otherwise.
    abstract member IsInfected: bool
    
    // Check if this computer can be infected.
    abstract member CanThisComputerBeInfected: bool
    
    // Os of this computer.
    abstract member Os: Os