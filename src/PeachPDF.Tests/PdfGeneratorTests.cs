using PeachPDF.PdfSharpCore;

namespace PeachPDF.Tests;

[TestFixture]
public class PdfGeneratorTests
{
    private PdfGenerator _generator = null!;

    [SetUp]
    public void Setup()
    {
        _generator = new PdfGenerator();
    }

    [Test]
    public async Task GeneratePdf_WithSimpleHtml_CreatesPdfDocument()
    {
        // Arrange
        var html = "<html><body><p>Hello World</p></body></html>";
        var config = new PdfGenerateConfig { PageSize = PageSize.Letter };

        // Act
        var document = await _generator.GeneratePdf(html, config);

        // Assert
        Assert.That(document, Is.Not.Null);
        Assert.That(document.PageCount, Is.GreaterThan(0));
    }

    [Test]
    public async Task GeneratePdf_WithPageSize_UsesCorrectPageSize()
    {
        // Arrange
        var html = "<html><body><p>Test</p></body></html>";
        var config = new PdfGenerateConfig { PageSize = PageSize.A4 };

        // Act
        var document = await _generator.GeneratePdf(html, config);

        // Assert
        Assert.That(document.Pages[0].Width.Point, Is.EqualTo(595.0).Within(1.0));
        Assert.That(document.Pages[0].Height.Point, Is.EqualTo(842.0).Within(1.0));
    }

    [Test]
    public async Task GeneratePdf_WithLandscapeOrientation_SwapsDimensions()
    {
        // Arrange
        var html = "<html><body><p>Test</p></body></html>";
        var config = new PdfGenerateConfig
        {
            PageSize = PageSize.Letter,
            PageOrientation = PageOrientation.Landscape
        };

        // Act
        var document = await _generator.GeneratePdf(html, config);

        // Assert
        Assert.That(document.Pages[0].Width.Point, Is.GreaterThan(document.Pages[0].Height.Point));
    }

    [Test]
    public async Task GeneratePdf_WithMargins_AppliesMargins()
    {
        // Arrange
        var html = "<html><body><p>Test</p></body></html>";
        var config = new PdfGenerateConfig { PageSize = PageSize.Letter };
        config.SetMargins(50);

        // Act
        var document = await _generator.GeneratePdf(html, config);

        // Assert
        Assert.That(config.MarginTop, Is.EqualTo(50));
        Assert.That(config.MarginBottom, Is.EqualTo(50));
        Assert.That(config.MarginLeft, Is.EqualTo(50));
        Assert.That(config.MarginRight, Is.EqualTo(50));
        Assert.That(document, Is.Not.Null);
    }

    [Test]
    public async Task GeneratePdf_WithStyledHtml_CreatesPdfDocument()
    {
        // Arrange
        var html = """
            <html>
            <head>
                <style>
                    body { font-family: Arial; }
                    h1 { color: blue; }
                    p { margin: 10px; }
                </style>
            </head>
            <body>
                <h1>Styled Heading</h1>
                <p>Styled paragraph with margins.</p>
            </body>
            </html>
            """;
        var config = new PdfGenerateConfig { PageSize = PageSize.Letter };

        // Act
        var document = await _generator.GeneratePdf(html, config);

        // Assert
        Assert.That(document, Is.Not.Null);
        Assert.That(document.PageCount, Is.GreaterThan(0));
    }

    [Test]
    public async Task GeneratePdf_WithFontFaceAttribute_CreatesPdfDocument()
    {
        // Arrange
        var html = """
            <html>
            <body>
                <font face="Arial">Arial text</font>
                <font face="Times New Roman">Times text</font>
            </body>
            </html>
            """;
        var config = new PdfGenerateConfig { PageSize = PageSize.Letter };

        // Act
        var document = await _generator.GeneratePdf(html, config);

        // Assert
        Assert.That(document, Is.Not.Null);
        Assert.That(document.PageCount, Is.GreaterThan(0));
    }

    [Test]
    public async Task GeneratePdf_WithTable_CreatesPdfDocument()
    {
        // Arrange
        var html = """
            <html>
            <body>
                <table border="1">
                    <tr><th>Header 1</th><th>Header 2</th></tr>
                    <tr><td>Cell 1</td><td>Cell 2</td></tr>
                    <tr><td>Cell 3</td><td>Cell 4</td></tr>
                </table>
            </body>
            </html>
            """;
        var config = new PdfGenerateConfig { PageSize = PageSize.Letter };

        // Act
        var document = await _generator.GeneratePdf(html, config);

        // Assert
        Assert.That(document, Is.Not.Null);
        Assert.That(document.PageCount, Is.GreaterThan(0));
    }

    [Test]
    public async Task GeneratePdf_WithEmptyHtml_ReturnsEmptyDocument()
    {
        // Arrange
        var html = "";
        var config = new PdfGenerateConfig { PageSize = PageSize.Letter };

        // Act
        var document = await _generator.GeneratePdf(html, config);

        // Assert
        Assert.That(document, Is.Not.Null);
        Assert.That(document.PageCount, Is.EqualTo(0));
    }

    [Test]
    public async Task GeneratePdf_SaveToStream_WritesData()
    {
        // Arrange
        var html = "<html><body><p>Hello World</p></body></html>";
        var config = new PdfGenerateConfig { PageSize = PageSize.Letter };

        // Act
        var document = await _generator.GeneratePdf(html, config);
        using var stream = new MemoryStream();
        document.Save(stream);

        // Assert
        Assert.That(stream.Length, Is.GreaterThan(0));
    }

    [Test]
    public async Task AddPdfPages_AppendsToExistingDocument()
    {
        // Arrange
        var html1 = "<html><body><p>Page 1</p></body></html>";
        var html2 = "<html><body><p>Page 2</p></body></html>";
        var config = new PdfGenerateConfig { PageSize = PageSize.Letter };

        // Act
        var document = await _generator.GeneratePdf(html1, config);
        var initialPageCount = document.PageCount;
        await _generator.AddPdfPages(document, html2, config);

        // Assert
        Assert.That(document.PageCount, Is.GreaterThan(initialPageCount));
    }


    [Test]
    public async Task FontFace_WithMultipleFonts_CreatesPdfWithCorrectPageCount()
    {
        // Arrange
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
        var config = new PdfGenerateConfig
        {
            PageSize = PageSize.Letter,
            PageOrientation = PageOrientation.Portrait
        };

        // Act
        var document = await _generator.GeneratePdf(html, config);

        // Assert
        Assert.That(document, Is.Not.Null);
        Assert.That(document.PageCount, Is.GreaterThanOrEqualTo(1));

        // Verify PDF can be saved successfully
        using var stream = new MemoryStream();
        document.Save(stream);
        Assert.That(stream.Length, Is.GreaterThan(0));
    }
}
