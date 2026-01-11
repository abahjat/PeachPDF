using PeachPDF.PdfSharpCore;

namespace PeachPDF.Tests;

[TestFixture]
public class PdfGenerateConfigTests
{
    [Test]
    public void SetMargins_WithPositiveValue_SetsAllMargins()
    {
        // Arrange
        var config = new PdfGenerateConfig();

        // Act
        config.SetMargins(25);

        // Assert
        Assert.That(config.MarginTop, Is.EqualTo(25));
        Assert.That(config.MarginBottom, Is.EqualTo(25));
        Assert.That(config.MarginLeft, Is.EqualTo(25));
        Assert.That(config.MarginRight, Is.EqualTo(25));
    }

    [Test]
    public void SetMargins_WithNegativeValue_DoesNotChangeMargins()
    {
        // Arrange
        var config = new PdfGenerateConfig();
        config.SetMargins(20);

        // Act
        config.SetMargins(-5);

        // Assert
        Assert.That(config.MarginTop, Is.EqualTo(20));
        Assert.That(config.MarginBottom, Is.EqualTo(20));
        Assert.That(config.MarginLeft, Is.EqualTo(20));
        Assert.That(config.MarginRight, Is.EqualTo(20));
    }

    [Test]
    public void MarginTop_WithNegativeValue_DoesNotChange()
    {
        // Arrange
        var config = new PdfGenerateConfig { MarginTop = 15 };

        // Act
        config.MarginTop = -10;

        // Assert
        Assert.That(config.MarginTop, Is.EqualTo(15));
    }

    [Test]
    public void MarginBottom_WithNegativeValue_DoesNotChange()
    {
        // Arrange
        var config = new PdfGenerateConfig { MarginBottom = 15 };

        // Act
        config.MarginBottom = -10;

        // Assert
        Assert.That(config.MarginBottom, Is.EqualTo(15));
    }

    [Test]
    public void MarginLeft_WithNegativeValue_DoesNotChange()
    {
        // Arrange
        var config = new PdfGenerateConfig { MarginLeft = 15 };

        // Act
        config.MarginLeft = -10;

        // Assert
        Assert.That(config.MarginLeft, Is.EqualTo(15));
    }

    [Test]
    public void MarginRight_WithNegativeValue_DoesNotChange()
    {
        // Arrange
        var config = new PdfGenerateConfig { MarginRight = 15 };

        // Act
        config.MarginRight = -10;

        // Assert
        Assert.That(config.MarginRight, Is.EqualTo(15));
    }

    [Test]
    public void DefaultDotsPerInch_Is72()
    {
        // Arrange & Act
        var config = new PdfGenerateConfig();

        // Assert
        Assert.That(config.DotsPerInch, Is.EqualTo(72));
    }

    [Test]
    public void DefaultMargins_Are10()
    {
        // Arrange & Act
        var config = new PdfGenerateConfig();

        // Assert
        Assert.That(config.MarginTop, Is.EqualTo(10));
        Assert.That(config.MarginBottom, Is.EqualTo(10));
        Assert.That(config.MarginLeft, Is.EqualTo(10));
        Assert.That(config.MarginRight, Is.EqualTo(10));
    }

    [Test]
    public void PageSize_CanBeSet()
    {
        // Arrange
        var config = new PdfGenerateConfig();

        // Act
        config.PageSize = PageSize.A4;

        // Assert
        Assert.That(config.PageSize, Is.EqualTo(PageSize.A4));
    }

    [Test]
    public void PageOrientation_CanBeSet()
    {
        // Arrange
        var config = new PdfGenerateConfig();

        // Act
        config.PageOrientation = PageOrientation.Landscape;

        // Assert
        Assert.That(config.PageOrientation, Is.EqualTo(PageOrientation.Landscape));
    }
}
