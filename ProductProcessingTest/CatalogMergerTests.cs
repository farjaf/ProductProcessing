using ProductProcessing.Models;
using ProductProcessing.Services.Implementation;

namespace ProductProcessingTest;

public class CatalogMergerTests
{
    [Fact]
    public void MergeCatalogs_ShouldMergeCorrectly()
    {
        // Arrange
        var catalogA = new List<Product>
        {
            new Product { SKU = "A1", Barcodes = new List<string> { "ABC123" } },
            new Product { SKU = "A2", Barcodes = new List<string> { "ABC124" } }
        };

        var catalogB = new List<Product>
        {
            new Product { SKU = "B1", Barcodes = new List<string> { "DEF123" } },
            // This product has a barcode that's already in catalog A, so it shouldn't appear in the merged catalog
            new Product { SKU = "B2", Barcodes = new List<string> { "ABC123" } }
        };

        var catalogService = new CatalogMerger();

        // Act
        var mergedCatalog = catalogService.MergeCatalogs(catalogA, catalogB);

        // Assert
        Assert.Equal(3, mergedCatalog.Count); // Should contain 3 unique products
        Assert.Contains(mergedCatalog, p => p.SKU == "A1");
        Assert.Contains(mergedCatalog, p => p.SKU == "A2");
        Assert.Contains(mergedCatalog, p => p.SKU == "B1");
        Assert.DoesNotContain(mergedCatalog, p => p.SKU == "B2"); // Product B2 should not be included
    }
}