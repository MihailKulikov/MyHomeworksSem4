module Program

open System

type LambdaTerm<'a> =
    | Variable of 'a
    | Application of LambdaTerm<'a> * LambdaTerm<'a>
    | Abstraction of 'a * LambdaTerm<'a>
    
let getSetOfFreeVariablesOf term =
    let rec loop acc term =
        match term with
        | Variable variable -> Set.add variable acc
        | Application (firstTerm, secondTerm) -> (loop acc firstTerm) + (loop acc secondTerm)
        | Abstraction (variable, innerTerm) -> Set.remove variable <| loop acc innerTerm
    
    loop Set.empty term
    
let rec getNewVariableNotBelongingTo set =
    let newVariable = Guid.NewGuid()
    if Set.contains newVariable set then getNewVariableNotBelongingTo set else newVariable
    
let rec substitution variableThatChanges substitutedTerm initialTerm =
    match initialTerm with
    | Variable variableInInitialTerm when variableInInitialTerm = variableThatChanges -> substitutedTerm
    | Variable _ -> initialTerm
    | Application (firstTerm, secondTerm) -> Application (substitution variableThatChanges substitutedTerm firstTerm,
                                                          substitution variableThatChanges substitutedTerm secondTerm)
    | Abstraction (variable, innerTerm) ->
        match substitutedTerm with
        | Variable _ -> initialTerm
        | _  when getSetOfFreeVariablesOf substitutedTerm |> Set.contains variable ||
                  getSetOfFreeVariablesOf innerTerm |> Set.contains variableThatChanges ->
            Abstraction (variable, substitution variableThatChanges substitutedTerm innerTerm)
        | _ -> let newVariable = getSetOfFreeVariablesOf innerTerm + getSetOfFreeVariablesOf substitutedTerm
                                 |> getNewVariableNotBelongingTo
               Abstraction (newVariable, innerTerm
                                         |> substitution variable (Variable newVariable)
                                         |> substitution variableThatChanges substitutedTerm)

let rec normalReduction term =
    match term with
    | Variable _ -> term
    | Application (firstTerm, secondTerm) ->
        match firstTerm with
        | Abstraction (variable, innerTerm) -> substitution variable secondTerm innerTerm
        | _ -> Application (normalReduction firstTerm, normalReduction secondTerm)
    | Abstraction (variable, innerTerm) -> Abstraction (variable, normalReduction innerTerm)
 
[<EntryPoint>]
let main argv =
    0 // return an integer exit code