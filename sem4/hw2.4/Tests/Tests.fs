module Tests

open NUnit.Framework
open Program
open FsUnit

[<TestCase(3, ExpectedResult = true)>]
[<TestCase(97, ExpectedResult = true)>]
[<TestCase(2, ExpectedResult = true)>]
[<TestCase(2147483647, ExpectedResult = true)>]
[<Test>]
let ``isPrime on primes numbers should return true`` (testCase: int) =
    isPrime <| bigint testCase

[<TestCase(4, ExpectedResult = false)>]
[<TestCase(341, ExpectedResult = false)>]
[<TestCase(221, ExpectedResult = false)>]
[<TestCase(2047, ExpectedResult = false)>]
let ``isPrime on composite numbers should return false`` (testCase: int) =
    isPrime <| bigint testCase
    
[<TestCase(0, ExpectedResult = false)>]
[<TestCase(1, ExpectedResult = false)>]
[<TestCase(-1, ExpectedResult = false)>]
[<TestCase(-101, ExpectedResult = false)>]
let ``interesting cases of not prime numbers`` (testCase: int) =
    isPrime <| bigint testCase
 
let ``first 10 elements of prime sequence should be correct`` =
    primes |> Seq.take 10 |> Seq.toList |> should equal [2, 3, 5, 7, 11, 13, 17, 19, 23, 29]