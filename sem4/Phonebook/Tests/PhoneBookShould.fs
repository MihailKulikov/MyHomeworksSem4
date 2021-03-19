module Tests

open System.IO
open System.Text.Json
open NUnit.Framework
open Program
open FsCheck
open FsUnit

type Tests() =
    [<Test>]
    member this.``Add new record`` () =
        let addEquivalentToAdd newRecord records =
            (newRecord :: records)
            |> List.except (addRecord newRecord records)
            |> List.length = 0
        Check.QuickThrowOnFailure addEquivalentToAdd
    
    [<Test>]
    member this.``Find record by name`` () =
        let property name (records: Record list) =
            records |> List.where (fun record -> record.Name = name) = findRecordByName name records
        Check.QuickThrowOnFailure property
        
    [<Test>]
    member this.``Find record by phone`` () =
        let property phoneNumber records =
            records
            |> List.where (fun record -> record.PhoneNumber = phoneNumber) = findRecordByPhoneNumber phoneNumber records
        Check.QuickThrowOnFailure property
        
    [<Test>]
    member this.``Get record string`` () =
        let property (record: Record) =
            $"{record.Name} : {record.PhoneNumber}" = getRecordString record
        Check.QuickThrowOnFailure property
    
    [<Test>]
    member this.``Get records string`` () =
        let property (records: Record list) =
            records
            |> List.map getRecordString
            |> List.fold (fun first second -> first + second) "" = getRecordsString records
        Check.QuickThrowOnFailure property
    
    [<Test>]
    member this.``Save to file`` () =
        let testPath = "test.txt"
        let records = [{Name = "kek"; PhoneNumber = "42"}]
        saveToFile testPath records
        File.ReadAllText(testPath) |> should equal <| JsonSerializer.Serialize records
        File.Delete testPath
        
    [<Test>]
    member this.``Return none then trying to read records from non existent file`` () =
        let testPath = "kek.txt"
        readFromFile testPath |> should equal None
        
    [<Test>]
    member this.``Read records from file`` () =
        let testPath = "test.txt"
        let records = [{Name = "kek"; PhoneNumber = "42"}]
        use writer = File.CreateText(testPath)
        records |> JsonSerializer.Serialize |> writer.Write
        writer.Dispose()
        
        readFromFile testPath |> should equal <| Some records
        File.Delete testPath
        
    [<Test>]
    member this.``Return none when trying execute add with incorrect command`` () =
        let records = [{Name = "kek"; PhoneNumber = "42"}]
        records |> add "Add forgotnumber" |> should equal None
        
    [<Test>]
    member this.Add () =
        let records = [{Name = "kek"; PhoneNumber = "42"}]
        let name = "ded"
        let phoneNumber = "54"
        add $"Add {name} : {phoneNumber}" records |> should equal <| Some ({Name = "ded"; PhoneNumber = "54"} :: records)
        
    [<Test>]
    member this.``Display the found by name records correctly`` () =
        let records = [{Name = "kek"; PhoneNumber = "42"}; {Name = "kek"; PhoneNumber = "54"}]
        let name = "kek"
        findByName $"Find by name {name}" records |> should equal <| Some records
        
    [<Test>]
    member this.``Return None when trying execute find by name with incorrect command`` () =
        findByName "kek" [] |> should equal None
        
    [<Test>]
    member this.``Not change collection after find by name`` () =
        let records = [{Name = "kek"; PhoneNumber = "42"}; {Name = "kek"; PhoneNumber = "54"}]
        findByName "Find by name kek" records |> should equal <| Some records
        
    [<Test>]
    member this.``Return empty when trying execute find by phone number with incorrect command`` () =
        findByPhoneNumber "kek" [] |> should equal <| None
    
    [<Test>]
    member this.``Not change collection after find by phone number`` () =
        let records = [{Name = "kek"; PhoneNumber = "42"}; {Name = "kek"; PhoneNumber = "54"}]
        findByPhoneNumber "Find by name rt" records |> should equal <| Some records
    
    [<Test>]
    member this.Save () =
        let path = "kek.txt"
        let records = [{Name = "kek"; PhoneNumber = "42"}; {Name = "kek"; PhoneNumber = "54"}]
        save $"Save {path}" records |> should equal <| Some records
        File.Delete path
    
    [<Test>]
    member this.``Return None when trying execute save with incorrect command`` () =
        save "save" [] |> should equal None
        
    [<Test>]
    member this.``Return empty when trying execute read with incorrect command`` () =
        read "read" [] |> should equal None
        
    [<Test>]
    member this.``Update records if handler returns Some`` () =
        let handler _ _ = Some []
        execute handler "kek" [{Name="kek"; PhoneNumber="Something"}] |> should equal []
        
    [<Test>]
    member this.``Should not update records if handler returns None`` () =
        let handler _ _ = None
        let records = [{Name="kek"; PhoneNumber="Something"}]
        execute handler "kek" records |> should equal records