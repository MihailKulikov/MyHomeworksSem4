module Tests

open NUnit.Framework
open Program
open FsUnit

let html = """<td><a href="regular-expression-language-quick-reference" data-linktype="relative-path">Regular Expression Language - Quick Reference</a></td>
<td>Provides information on the set of characters, operators, and constructs that you can use to define regular expressions.</td>
</tr>
<tr>
<td><a href="the-regular-expression-object-model" data-linktype="relative-path">The Regular Expression Object Model</a></td>
<td>Provides information and code examples that illustrate how to use the regular expression classes.</td>
</tr>
<tr>
<td><a href="details-of-regular-expression-behavior" data-linktype="relative-path">Details of Regular Expression Behavior</a></td>
<td>Provides information about the capabilities and behavior of .NET regular expressions.</td>
</tr>
<tr>
<td><a href="/en-us/visualstudio/ide/using-regular-expressions-in-visual-studio" data-linktype="absolute-path">Use regular expressions in Visual Studio</a></td>
<td></td>
</tr>
</tbody>
</table>
<h2 id="reference">Reference</h2>
<ul>
<li><a href="/en-us/dotnet/api/system.text.regularexpressions" data-linktype="absolute-path">System.Text.RegularExpressions</a></li>
<li><a href="/en-us/dotnet/api/system.text.regularexpressions.regex" data-linktype="absolute-path">System.Text.RegularExpressions.Regex</a></li>
<li><a href="https://download.microsoft.com/download/D/2/4/D240EBF6-A9BA-4E4F-A63F-AEB6DA0B921C/Regular%20expressions%20quick%20reference.docx" data-linktype="external">Regular Expressions - Quick Reference (download in Word format)</a></li>
<li><a href="https://download.microsoft.com/download/D/2/4/D240EBF6-A9BA-4E4F-A63F-AEB6DA0B921C/Regular%20expressions%20quick%20reference.pdf" data-linktype="external">Regular Expressions - Quick Reference (download in PDF format)</a></li>
</ul>"""

[<SetUp>]
let Setup () =
    ()

[<Test>]
let ShouldCorrectlyGetAllUrls () =
    let actual = ["https://download.microsoft.com/download/D/2/4/D240EBF6-A9BA-4E4F-A63F-AEB6DA0B921C/Regular%20expressions%20quick%20reference.docx"
                  "https://download.microsoft.com/download/D/2/4/D240EBF6-A9BA-4E4F-A63F-AEB6DA0B921C/Regular%20expressions%20quick%20reference.pdf"]
    html |> getAllUrlsFrom |> should equivalent actual
    
