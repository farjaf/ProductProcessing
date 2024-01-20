using Moq;
using ProductProcessing.Models;
using ProductProcessing.Services.Implementation;
using ProductProcessing.Services.Interfaces;

namespace ProductProcessingTest
{
    public class DataLoaderTests
    {
        [Fact]
        public void LoadData_ShouldReturnCorrectCatalog()
        {
            // Arrange
            var mockCsvReaderService = new Mock<ICsvReaderService>();
            mockCsvReaderService.Setup(m => m.ReadCsvFile<CatalogRecord>(It.IsAny<string>())).Returns(new List<CatalogRecord>
            {
                new() { SKU = "123", Description = "Product 123" },
                // ... other records ...
            });

            var barcodes = new Dictionary<string, List<string>> { { "123", ["ABC123"] } };
            var dataLoader = new DataLoader(mockCsvReaderService.Object);

            // Act
            var result = dataLoader.LoadCatalog("dummy.csv", "A", barcodes);

            // Assert
            Assert.Single(result);
            Assert.Equal("123", result.First().SKU);
            Assert.Equal("Product 123", result.First().Description);
        }

        [Fact]
        public void LoadData_ShouldReturnCorrectBarcodeGrouping()
        {
            // Arrange
            var mockCsvReaderService = new Mock<ICsvReaderService>();
            mockCsvReaderService.Setup(m => m.ReadCsvFile<BarcodeRecord>(It.IsAny<string>())).Returns(new List<BarcodeRecord>
            {
                new() { SKU = "123", Barcode = "ABC123" },
                new() { SKU = "123", Barcode = "ABC124" },
            });

            var dataLoader = new DataLoader(mockCsvReaderService.Object);

            // Act
            var result = dataLoader.LoadBarcodes("dummy.csv");

            // Assert
            Assert.Equal(2, result["123"].Count);
            Assert.Contains("ABC123", result["123"]);
            Assert.Contains("ABC124", result["123"]);
        }
    }
}