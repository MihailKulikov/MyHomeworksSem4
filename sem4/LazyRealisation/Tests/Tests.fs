module Tests

open Foq
open LazyRealisation
open NUnit.Framework
open FsUnit

type LazyType =
    | SingleThreaded = 0
    | MultiThreaded = 1

type IObjectFactory =
    abstract member GetNewObject: unit -> obj
    
let getObjectFactoryMock () = Mock<IObjectFactory>()

[<Test>]
let ``Should calculate and return value for the first call get method`` () =
    let expectedObject = obj()
    let objectFactoryMock = getObjectFactoryMock()
    let objectFactory = getObjectFactoryMock()
                            .Setup(fun factory -> <@ factory.GetNewObject() @>)
                            .Returns(expectedObject)
                            .Create()
    let myLazy = LazyFactory.CreateSingleThreadedLazy(objectFactory.GetNewObject)
    let actualObject = myLazy.Get()
    
    verify <@ objectFactory.GetNewObject() @> once
    actualObject |> should equal expectedObject
    