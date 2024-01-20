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
            new() { SKU = "A1", Barcodes = ["ABC123"] },
            new() { SKU = "A2", Barcodes = ["ABC124"] }
        };

        var catalogB = new List<Product>
        {
            new() { SKU = "B1", Barcodes = ["DEF123"] },
            // This product has a barcode that's already in catalog A, so it shouldn't appear in the merged catalog
            new() { SKU = "B2", Barcodes = ["ABC123"] }
        };

        var catalogService = new CatalogMerger();

        // Act
        var mergedCatalog = catalogService.MergeCatalogs(catalogA, catalogB);

        // Assert
        Assert.Equal(3, mergedCatalog.Count);
        Assert.Contains(mergedCatalog, p => p.SKU == "A1");
        Assert.Contains(mergedCatalog, p => p.SKU == "A2");
        Assert.Contains(mergedCatalog, p => p.SKU == "B1");

        Assert.DoesNotContain(mergedCatalog, p => p.SKU == "B2"); // Product B2 should not be included
    }
}