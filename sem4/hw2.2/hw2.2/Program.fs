module Program

/// <summary>
/// Represents binary tree.
/// </summary>
type Tree<'a> =
    | Node of 'a * Tree<'a> * Tree<'a>
    | Empty
    
/// <summary>Builds a new binary tree whose elements are the results of applying the given function
/// to each of the elements of the binary tree.</summary>
/// <param name="mapping">The function to transform elements from the input binary tree.</param>
/// <param name="binaryTree">The input binary tree.</param>
/// <returns>The binary tree of transformed elements.</returns>
let rec map mapping binaryTree =
    match binaryTree with
    | Empty -> Empty
    | Node(value, leftChild, rightChild) -> Node(mapping(value), map mapping leftChild, map mapping rightChild)

[<EntryPoint>]
let main _ =
    0