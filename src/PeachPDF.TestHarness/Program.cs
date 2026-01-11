// See https://aka.ms/new-console-template for more information
using PeachPDF;
using PeachPDF.Network;
using PeachPDF.PdfSharpCore;

var fileName = "face-test";
var html = """
<!DOCTYPE html>
<html>
<head>
    <title>Font Face Attribute Test</title>
</head>
<body>
    <h1>Font Face Attribute Test</h1>

    <p>Default font paragraph.</p>

    <font face="Arial">This text uses Arial via face attribute.</font>
    <br><br>

    <font face="Times New Roman">This text uses Times New Roman via face attribute.</font>
    <br><br>

    <font face="Courier New, monospace">This text uses Courier New via face attribute.</font>
    <br><br>

    <font face="Georgia" size="5" color="blue">
        This text uses Georgia, size 5, and blue color via legacy font tag attributes.
    </font>
    <br><br>

    <p style="font-family: Verdana;">This text uses Verdana via CSS style attribute.</p>
</body>
</html>
""";

using var httpClient = new HttpClient();

PdfGenerateConfig pdfConfig = new()
{
    PageSize = PageSize.Letter,
    PageOrientation = PageOrientation.Portrait,
    NetworkLoader = new HttpClientNetworkLoader(httpClient, null)
};

var stream = new MemoryStream();
var pdfGenerator = new PdfGenerator();
var document = await pdfGenerator.GeneratePdf(html, pdfConfig);
document.Save(stream);

File.Delete($"{fileName}.pdf");
File.WriteAllBytes($"{fileName}.pdf", stream.ToArray());

Console.WriteLine($"Generated {fileName}.pdf ({stream.Length} bytes)");
