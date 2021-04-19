module Program

open System
open System.IO
open System.Net
open System.Text.RegularExpressions

/// Gets the html document at the specified url asynchronously.
let asyncGetHtmlStringFrom (url:string) =
    async {
        let request = WebRequest.Create(url)
        use! response = request.AsyncGetResponse()
        use stream = response.GetResponseStream()
        use reader = new StreamReader(stream)
        let htmlString = reader.ReadToEnd()
        return htmlString
    }

/// Gets all urls from the given html document.
let getAllUrlsFrom htmlString =
    let pattern = """\s*(?i)href\s*=\s*(\"(http[^"]*)\"|'[^']*'|([^'">\s]+))"""
    (Regex.Matches(htmlString, pattern))
    |> Seq.map (fun regMatch  -> regMatch.Groups.[2].Value)
    |> Seq.filter (fun url -> url <> "")

/// Crawls specified url.
let crawl url =
    let urls = url |> asyncGetHtmlStringFrom |> Async.RunSynchronously |> getAllUrlsFrom
    urls
    |> Seq.map asyncGetHtmlStringFrom
    |> Async.Parallel
    |> Async.RunSynchronously
    |> Array.toSeq
    |> Seq.map (fun (htmlString: string) -> htmlString.Length)
    |> Seq.zip urls

/// Prints result of crawl function to console.
let printResult result =
    Seq.iter (fun urlResult -> printf $"%A{fst urlResult} -- %A{snd urlResult}\n") result

[<EntryPoint>]
let main _ =
    let request = Console.ReadLine()
    request |> crawl |> printResult
    0