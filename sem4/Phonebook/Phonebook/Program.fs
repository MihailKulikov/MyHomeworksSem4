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
    | None -> printf "%s\n" "There is no such element."
        
let startCli =
    printf "%s" introduceMessage
    let rec loop userInput records =
        match userInput with
        | exitCode when exitCode = "Exit" -> records
        | addCode when addCode.StartsWith("Add") ->
            let input = addCode.Split([|" : "; " "|], StringSplitOptions.None).[1..]
            let name = input.[0]
            let phoneNumber = input.[1]
            records |> addRecord {Name = name; PhoneNumber = phoneNumber} |> (loop <| Console.ReadLine())
        | findByNameCode when findByNameCode.StartsWith("Find by name") ->
            let name = findByNameCode.Split(' ') |> Array.last
            records |> findRecordByName name |> tryPrintRecord
            loop (Console.ReadLine()) records
        | findByPhoneCode when findByPhoneCode.StartsWith("Find by phone") ->
            let phoneNumber = findByPhoneCode.Split(' ') |> Array.last
            records |> findRecordByPhoneNumber phoneNumber |> tryPrintRecord
            loop (Console.ReadLine()) records
        | getAllCode when getAllCode.StartsWith("Get all") ->
            records |> getRecordsString |> printf "%s\n"
            loop (Console.ReadLine()) records
        | saveCode when saveCode.StartsWith("Save") ->
            let path = saveCode.Split(' ') |> Array.last
            records |> saveToFile path
            loop (Console.ReadLine()) records
        | readCode when readCode.StartsWith("Read") ->
            match readCode.Split(' ') |> Array.last |> readFromFile with
            | Some(readRecords) -> loop (Console.ReadLine()) readRecords
            | None ->
                printf "%s" "File not found.\n"
                loop (Console.ReadLine()) records
        | _ ->
            printf "%s" "Incorrect command.\n"
            loop (Console.ReadLine()) records
    loop (Console.ReadLine()) List.empty |> ignore
        
[<EntryPoint>]
let main _ =
    startCli
    0