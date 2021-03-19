module Tests

open NUnit.Framework
open Program
open FsCheck
open FsUnit

type Tests() =
    
    [<Test>]
    member this.addNewRecord () =
        let addEquivalentToAdd newRecord records =
            (newRecord :: records)
            |> List.except (addRecord newRecord records)
            |> List.length = 0
        Check.QuickThrowOnFailure addEquivalentToAdd
    
    [<Test>]
    member this.findRecordByName () =
        let property name (records: Record list) =
            records |> List.tryFind (fun record -> record.Name = name) = findRecordByName name records
        Check.QuickThrowOnFailure property
        
    [<Test>]
    member this.findRecordByPhone () =
        let property phoneNumber records =
            records |> List.tryFind (fun record -> record.PhoneNumber = phoneNumber) = findRecordByPhoneNumber name records
        Check.QuickThrowOnFailure property
        
    [<Test>]
    member this.getRecordString () 