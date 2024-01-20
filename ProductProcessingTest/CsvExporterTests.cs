using Moq;
using ProductProcessing.Models;
using System.IO.Abstractions;
using ProductProcessing.Services.Implementation;
using System.Text;

namespace ProductProcessingTest;

public class CsvExporterTests
{
    [Fact]
    public void ExportToCsv_ShouldCreateDirectoryAndFile()
    {
        // Arrange
        var mockFileSystem = new Mock<IFileSystem>();
        var mockFileBase = new Mock<FileBase>();
        var mockDirectoryBase = new Mock<DirectoryBase>();
        var mockPathBase = new Mock<PathBase>();
        var mockStreamWriter = new Mock<StreamWriter>(MockBehavior.Loose, Stream.Null, Encoding.UTF8);

        mockFileSystem.Setup(fs => fs.File).Returns(mockFileBase.Object);
        mockFileSystem.Setup(fs => fs.Directory).Returns(mockDirectoryBase.Object);
        mockFileSystem.Setup(fs => fs.Path).Returns(mockPathBase.Object);

        mockPathBase.Setup(p => p.GetDirectoryName(It.IsAny<string>())).Returns("test_dir");
        mockDirectoryBase.Setup(d => d.Exists("test_dir")).Returns(false);
        mockFileBase.Setup(f => f.CreateText(It.IsAny<string>())).Returns(mockStreamWriter.Object);

        var csvExporter = new CsvExporter(mockFileSystem.Object);

        var catalog = new List<Product>();

        // Act
        csvExporter.ExportToCsv(catalog, "test.csv");

        // Assert
        mockDirectoryBase.Verify(d => d.CreateDirectory("test_dir"), Times.Once);
        mockFileBase.Verify(f => f.CreateText("test.csv"), Times.Once);
    }
}