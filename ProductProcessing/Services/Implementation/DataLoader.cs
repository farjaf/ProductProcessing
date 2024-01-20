using ProductProcessing.Models;
using ProductProcessing.Services.Interfaces;

namespace ProductProcessing.Services.Implementation;

public class DataLoader(ICsvReaderService fileReader) : IDataLoader
{
    public Dictionary<string, List<string>> LoadBarcodes(string filePath)
    {
        var records = fileReader.ReadCsvFile<BarcodeRecord>(filePath);

        return records
            .GroupBy(b => b.SKU)
            .ToDictionary(group => group.Key, group => group.Select(b => b.Barcode).ToList());
    }

    public List<Product> LoadCatalog(string filePath, string company, Dictionary<string, List<string>> barcodes)
    {
        var records = fileReader.ReadCsvFile<CatalogRecord>(filePath);

        return records.Select(r => new Product
            {
                SKU = r.SKU,
                Description = r.Description,
                Source = company,
                Barcodes = barcodes.TryGetValue(r.SKU, out var barcode) ? barcode : []
            })
            .ToList();
    }
}