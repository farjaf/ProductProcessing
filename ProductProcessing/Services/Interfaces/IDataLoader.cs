using ProductProcessing.Models;

namespace ProductProcessing.Services.Interfaces;

public interface IDataLoader
{
    Dictionary<string, List<string>> LoadBarcodes(string filePath);

    List<Product> LoadCatalog(string filePath, string company, Dictionary<string, List<string>> barcodes);
}