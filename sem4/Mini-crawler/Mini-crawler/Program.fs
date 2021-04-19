open System
open System.IO
open System.Net
open System.Text.RegularExpressions

let asyncGetHtmlFrom (url:string) =
    async {
        let request = WebRequest.Create(url)
        use! response = request.AsyncGetResponse()
        use stream = response.GetResponseStream()
        use reader = new StreamReader(stream)
        let htmlString = reader.ReadToEnd()
        return htmlString
    }

let getAllUrlsFrom htmlString =
    let pattern = """\s*(?i)href\s*=\s*(\"(http[^"]*)\"|'[^']*'|([^'">\s]+))"""
    (Regex.Matches(htmlString, pattern))
    |> Seq.map (fun regMatch  -> regMatch.Groups.[2].Value)
    |> Seq.filter (fun url -> url <> "")

let crawl url =
    let urls = url |> asyncGetHtmlFrom |> Async.RunSynchronously |> getAllUrlsFrom
    urls
    |> Seq.map asyncGetHtmlFrom
    |> Async.Parallel
    |> Async.RunSynchronously
    |> Array.toSeq
    |> Seq.map (fun (htmlString: string) -> htmlString.Length)
    |> Seq.zip urls

let printResult result =
    Seq.iter (fun urlResult -> printf $"%A{fst urlResult} -- %A{snd urlResult}\n") result

[<EntryPoint>]
let main _ =
    let request = Console.ReadLine()
    request |> crawl |> printResult
    0