module Tests

open System.Numerics
open NUnit.Framework
open Program

[<Test>]
let Test1 () =
    Assert.That(sumOfEvenFibonacciNumbersSmallerThan max, Is.EqualTo(BigInteger(4613732)))