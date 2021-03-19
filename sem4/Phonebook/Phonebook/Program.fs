module Program

open System
open System.IO
open System.Text.Json

let introduceMessage = """
Hello, user! Nice to see you! Use app by following this instructions:
1) If you want to close the application, then just write "Exit"
2) For adding new record, write a record in the format "Add {Name} : {Phone number}"
3) For finding a record with the specified name, write "Find by name {Name}"
4) For finding a record with the specified phone number, write "Find by phone {Phone number}"
5) For getting all records, write "Get all"
6) For saving records to a file with the specified path, write "Save {Path}"
7) For reading records from file with the specified path, write "Read {Path}"
"""

let addKey = "Add"
let exitKey = "Exit"
let findByNameKey = "Find by name"
let findByPhoneKey = "Find by phone"
let getAllKey = "Get all"
let saveKey = "Save"
let readKey = "Read"
let incorrectCommandMessage = "Incorrect command."
let recordNotFoundMessage = "There is no such record."

type Record =
    {
        Name: string;
        PhoneNumber: string;
    }

let addRecord newRecord records =
    newRecord :: records

let findRecordByName name records =
    List.tryFind (fun record -> record.Name = name) records
    
let findRecordByPhoneNumber phoneNumber records =
    List.tryFind (fun record -> record.PhoneNumber = phoneNumber) records
    
let getRecordString record =
    $"{record.Name} : {record.PhoneNumber}"

let getRecordsString records =
    records
    |> List.map getRecordString
    |> List.fold (fun acc recordString -> acc + recordString) ""

let saveToFile path records =
    use fileStream = new FileStream(path, FileMode.Create)
    let json = JsonSerializer.SerializeToUtf8Bytes(records)
    fileStream.Write(json, 0, json.Length)
    
let readFromFile path =
    if File.Exists path
    then
        path |> File.ReadAllText |> JsonSerializer.Deserialize<Record array> |> Array.toList |> Some
    else
        None
    
let tryPrintRecord recordOption =
    match recordOption with
    | Some(record) -> record |> getRecordString |> printf "%s\n"
    | None -> printf "%s\n" recordNotFoundMessage
        
let executeAdd (request: string) records =
    let input = request.Split([|" : "; " "|], StringSplitOptions.None).[1..]
    if Array.length input <> 2
    then
        None
    else
        let name = input.[0]
        let phoneNumber = input.[1]
        records |> addRecord {Name = name; PhoneNumber = phoneNumber} |> Some
    
let executeFindByName (request: string) records =
    let input = request.Split(' ')
    if Array.length input < 4
    then
        None
    else
        let name = input.[3..] |> Array.fold (fun acc partOfName -> acc + partOfName) ""
        records |> findRecordByName name |> tryPrintRecord
        Some records

let executeFindByPhoneNumber (request: string) records =
    let phoneNumber = request.Split(' ') |> Array.last
    records |> findRecordByName phoneNumber |> tryPrintRecord
    
let executeSave (request: string) records =
    let path = request.Split(' ') |> Array.last
    records |> saveToFile path

let executeRead (request: string) =
    request.Split(' ') |> Array.last |> readFromFile

let executeRequest execute request records =
        match execute request records with
            | Some (updatedRecords) -> updatedRecords
            | None ->
                printf "%s\n" "Incorrect command."
                records

let startCli =
    printf "%s" introduceMessage
    let rec loop userInput records =
        match userInput with
        | exitRequest when  exitRequest = exitKey -> records
        | addRequest when addRequest.StartsWith(addKey) ->
            records |> executeRequest executeAdd addRequest |> loop (Console.ReadLine())
        | findByNameRequest when findByNameRequest.StartsWith(findByNameKey) ->
            records |> executeRequest executeFindByName findByNameRequest |> loop (Console.ReadLine())
        | findByPhoneRequest when findByPhoneRequest.StartsWith("Find by phone") ->
            let phoneNumber = findByPhoneRequest.Split(' ') |> Array.last
            records |> findRecordByPhoneNumber phoneNumber |> tryPrintRecord
            loop (Console.ReadLine()) records
        | getAllRequest when getAllRequest.StartsWith("Get all") ->
            records |> getRecordsString |> printf "%s\n"
            loop (Console.ReadLine()) records
        | saveRequest when saveRequest.StartsWith("Save") ->
            records |> executeSave saveRequest
            loop (Console.ReadLine()) records
        | readRequest when readRequest.StartsWith("Read") ->
            match executeRead readRequest with
            | Some (readRecords) -> loop (Console.ReadLine()) readRecords
            | None -> loop (Console.ReadLine()) records
        | _ ->
            printf "%s" "Incorrect command.\n"
            loop (Console.ReadLine()) records
    loop (Console.ReadLine()) List.empty |> ignore
        
[<EntryPoint>]
let main _ =
    startCli
    0