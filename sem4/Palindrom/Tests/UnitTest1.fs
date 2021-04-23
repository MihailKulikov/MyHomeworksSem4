module Tests

open NUnit.Framework
open FsUnit
open Program

[<Test>]
let regressionTest () =
    findMaxPalindrome() |> should equal 906609

[<Test>]
let palindromeShouldBePalindrome() =
    isPalindrome 121 |> should be True
    
[<Test>]
let notPalindromeShouldNotBePalindrome() =
    isPalindrome 42 |> should be False