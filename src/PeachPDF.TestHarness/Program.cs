// See https://aka.ms/new-console-template for more information
using PeachPDF;
using PeachPDF.PdfSharpCore;

var fileName = "rfc2324";
var html = File.ReadAllText($"{fileName}.html");

PdfGenerateConfig pdfConfig = new()
{
    PageSize = PageSize.Letter,
    PageOrientation = PageOrientation.Portrait
};

var stream = new MemoryStream();
var pdfGenerator = new PdfGenerator();
var document = await pdfGenerator.GeneratePdf(html, pdfConfig);
document.Save(stream);

File.Delete($"{fileName}.pdf");
File.WriteAllBytes($"{fileName}.pdf", stream.ToArray());

Console.WriteLine($"Generated {fileName}.pdf ({stream.Length} bytes, {document.PageCount} pages)");