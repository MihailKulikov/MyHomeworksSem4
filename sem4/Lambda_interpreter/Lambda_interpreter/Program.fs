module Program

open System

type LambdaTerm<'a> =
    | Variable of 'a
    | Application of LambdaTerm<'a> * LambdaTerm<'a>
    | Abstraction of 'a * LambdaTerm<'a>
    
let getSetOfFreeVariablesOf lambdaTerm =
    let rec loop acc lambdaTerm =
        match lambdaTerm with
        | Variable variable -> acc |> Set.add variable
        | Application (firstTerm, secondTerm) -> (loop acc firstTerm) + (loop acc secondTerm)
        | Abstraction (variable, innerTerm) -> (loop acc innerTerm) |> Set.remove variable
    
    loop Set.empty lambdaTerm
    
let rec getNewVariableNotBelongingTo set =
    let newVariable = Guid.NewGuid()
    if set |> Set.contains newVariable then getNewVariableNotBelongingTo set else newVariable
    
let rec substitution variableThatChanges substitutedTerm initialTerm =
    match initialTerm with
    | Variable variableInInitialTerm when variableInInitialTerm = variableThatChanges -> substitutedTerm
    | Variable _ -> initialTerm
    | Application (firstTerm, secondTerm) -> Application (firstTerm |> substitution variableThatChanges substitutedTerm,
                                                          secondTerm |> substitution variableThatChanges substitutedTerm)
    | Abstraction (variable, innerTerm)  ->
        match substitutedTerm with
        | Variable _ -> initialTerm
        | _  when getSetOfFreeVariablesOf substitutedTerm |> Set.contains variable ||
                  getSetOfFreeVariablesOf innerTerm |> Set.contains variableThatChanges ->
            Abstraction (variable, innerTerm |> substitution variableThatChanges substitutedTerm)
        | _ -> let newVariable = getNewVariableNotBelongingTo (getSetOfFreeVariablesOf innerTerm + getSetOfFreeVariablesOf substitutedTerm)
               Abstraction (newVariable, innerTerm
                                         |> substitution variable (Variable (newVariable))
                                         |> substitution variableThatChanges substitutedTerm)
 
[<EntryPoint>]
let main argv =
    0 // return an integer exit code