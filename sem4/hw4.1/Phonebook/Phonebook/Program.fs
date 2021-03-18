open System.IO
open System.Text.Json

let welcomeMessage = """
Hello, user! Nice to see you! Use me by following this instructions:
1) If you want to close the application, then just write "Exit"
2) For adding new record, write a record in the format "Add {Name} : {Phone number}"
3) For finding a record with the specified name, write "Find by name {Name}"
4) For finding a record with the specified phone number, write "Find by phone {Phone number}"
5) For getting all records, write "Get all"
6) For saving records to a file with the specified path, write "Save {Path}"
7) For reading records from file with the specified path, write "Read {Path}"

"""

type Record =
    {
        Name: string;
        PhoneNumber: string;
    }

let addRecord newRecord records =
    newRecord :: records

let findRecordByName name records =
    records |> List.tryFind (fun record -> record.Name = name)
    
let findRecordByPhoneNumber phoneNumber records =
    records |> List.tryFind (fun record -> record.PhoneNumber = phoneNumber)
    
let getRecordsString records =
    records
    |> List.map (fun record -> $"{record.Name} : {record.PhoneNumber}")
    |> List.fold (fun acc recordString -> acc + recordString + "\n") ""

let saveToFile path records =
    let fileStream = new FileStream(path, FileMode.Create)
    let json = JsonSerializer.SerializeToUtf8Bytes(records)
    fileStream.Write(json, 0, json.Length)
    
let readFromFile path =
    if File.Exists path
    then
        path |> File.ReadAllText |> JsonSerializer.Deserialize |> unbox |> Some
    else
        None
        
let startCli =
    
    let loop userInput records =
        match userInput with
        | exitCode -> when exitCode = "1"

[<EntryPoint>]
let main _ =
    0