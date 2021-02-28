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
    
let rec substitute variableThatChanges substitutedTerm initialTerm =
    match initialTerm with
    | Variable variableInInitialTerm when variableInInitialTerm = variableThatChanges -> substitutedTerm
    | Variable _ -> initialTerm
    | Application (firstTerm, secondTerm) -> Application (substitute variableThatChanges substitutedTerm firstTerm,
                                                          substitute variableThatChanges substitutedTerm secondTerm)
    | Abstraction (variable, innerTerm) ->
        match substitutedTerm with
        | Variable variable when variable = variableThatChanges -> initialTerm
        | _  when getSetOfFreeVariablesOf substitutedTerm |> Set.contains variable |> not ||
                  getSetOfFreeVariablesOf innerTerm |> Set.contains variableThatChanges |> not ->
            Abstraction (variable, substitute variableThatChanges substitutedTerm innerTerm)
        | _ -> let newVariable = getSetOfFreeVariablesOf innerTerm + getSetOfFreeVariablesOf substitutedTerm
                                 |> getNewVariableNotBelongingTo
               Abstraction (newVariable, innerTerm
                                         |> substitute variable (Variable newVariable)
                                         |> substitute variableThatChanges substitutedTerm)

let rec applyNormalReduction term =
    match term with
    | Variable _ -> term
    | Application (firstTerm, secondTerm) ->
        match firstTerm with
        | Abstraction (variable, innerTerm) -> applyNormalReduction <| substitute variable secondTerm innerTerm
        | _ -> Application (applyNormalReduction firstTerm, applyNormalReduction secondTerm)
    | Abstraction (variable, innerTerm) -> Abstraction (variable, applyNormalReduction innerTerm)
 
[<EntryPoint>]
let main _ =
    0