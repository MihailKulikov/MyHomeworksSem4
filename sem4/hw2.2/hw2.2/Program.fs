module Program

/// <summary>
/// Represents binary tree.
/// </summary>
type Tree<'a> =
    | Node of 'a * Tree<'a> * Tree<'a>
    | Leaf
    
/// <summary>Builds a new binary tree whose elements are the results of applying the given function
/// to each of the elements of the binary tree.</summary>
/// <param name="mapping">The function to transform elements from the input binary tree.</param>
/// <param name="binaryTree">The input binary tree.</param>
/// <returns>The binary tree of transformed elements.</returns>
let map mapping binaryTree =
    let rec bypass binTree =
        match binTree with
        | Leaf -> Leaf
        | Node(x, l, r) -> Node(mapping(x), bypass(l), bypass(r))
    
    bypass binaryTree

[<EntryPoint>]
let main _ =
    0