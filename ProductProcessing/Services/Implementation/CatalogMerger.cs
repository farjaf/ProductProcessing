using ProductProcessing.Models;
using ProductProcessing.Services.Interfaces;

namespace ProductProcessing.Services.Implementation;

public class CatalogMerger : ICatalogMerger
{
    public List<Product> MergeCatalogs(List<Product> catalogA, List<Product> catalogB)
    {
        // Create sets of barcodes for company A
        var barcodesA = new HashSet<string>(catalogA.SelectMany(p => p.Barcodes));

        var mergedCatalog = catalogA.ToList();

        // Add products from Company B that have unique barcodes not in Company A
        foreach (var product in catalogB)
        {
            // Only include the product if none of its barcodes are present in Company A's barcode set
            if (!product.Barcodes.Any(b => barcodesA.Contains(b)))
            {
                mergedCatalog.Add(product);
            }
        }

        return mergedCatalog;
    }
}