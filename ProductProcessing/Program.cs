// See https://aka.ms/new-console-template for more information

using System.IO.Abstractions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ProductProcessing.Services.Implementation;
using ProductProcessing.Services.Interfaces;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddSingleton<ICsvReaderService, CsvReaderService>();
        services.AddSingleton<IDataLoader, DataLoader>();
        services.AddSingleton<IFileSystem, FileSystem>();
        services.AddSingleton<ICatalogMerger, CatalogMerger>();
        services.AddSingleton<ICsvExporter, CsvExporter>();
    })
    .Build();

RunApplicationAsync(host.Services);
return;

void RunApplicationAsync(IServiceProvider serviceProvider)
{
    var dataLoader = serviceProvider.GetRequiredService<IDataLoader>();
    var catalogMerger = serviceProvider.GetRequiredService<ICatalogMerger>();
    var csvExporter = serviceProvider.GetRequiredService<ICsvExporter>();

    var barcodesA = dataLoader.LoadBarcodes("Input/barcodesA.csv");
    var barcodesB = dataLoader.LoadBarcodes("Input/barcodesB.csv");
    var catalogA = dataLoader.LoadCatalog("Input/catalogA.csv", "A", barcodesA);
    var catalogB = dataLoader.LoadCatalog("Input/catalogB.csv", "B", barcodesB);

    var mergedCatalog = catalogMerger.MergeCatalogs(catalogA, catalogB);
    csvExporter.ExportToCsv(mergedCatalog, "Output/result_output.csv");
}

//var barcodesA = LoadBarcodes("Input/barcodesA.csv");
//var barcodesB = LoadBarcodes("Input/barcodesB.csv");
//var catalogA = LoadCatalog("Input/catalogA.csv", "A", barcodesA);
//var catalogB = LoadCatalog("Input/catalogB.csv", "B", barcodesB);

//var mergedCatalog = MergeCatalogs(catalogA, catalogB);
//ExportToCsv(mergedCatalog, "Output/result.csv");

//return;


//static Dictionary<string, List<string>> LoadBarcodes(string filePath)
//{
//    using var reader = new StreamReader(filePath);
//    using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
//    var records = csv.GetRecords<BarcodeRecord>();

//    return records
//        .GroupBy(b => b.SKU)
//        .ToDictionary(group => group.Key, group => group.Select(b => b.Barcode).ToList());
//}

//static List<Product> LoadCatalog(string filePath, string company, Dictionary<string, List<string>> barcodes)
//{
//    using var reader = new StreamReader(filePath);
//    using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
//    var records = csv.GetRecords<CatalogRecord>();

//    return records.Select(r => new Product
//    {
//        SKU = r.SKU,
//        Description = r.Description,
//        Company = company,
//        Barcodes = barcodes.TryGetValue(r.SKU, out var barcode) ? barcode : []
//    })
//        .ToList();
//}

//static List<Product> MergeCatalogs(List<Product> catalogA, List<Product> catalogB)
//{
//    // Create sets of barcodes for each company
//    var barcodesA = new HashSet<string>(catalogA.SelectMany(p => p.Barcodes));
//    var barcodesB = new HashSet<string>(catalogB.SelectMany(p => p.Barcodes));

//    var mergedCatalog = catalogA.ToList();

//    // Add products from Company B that have unique barcodes not in Company A
//    foreach (var product in catalogB)
//    {
//        // Only include the product if none of its barcodes are present in Company A's barcode set
//        if (!product.Barcodes.Any(b => barcodesA.Contains(b)))
//        {
//            mergedCatalog.Add(product);
//        }
//    }

//    return mergedCatalog;
//}




//static void ExportToCsv(List<Product> catalog, string filePath)
//{
//    var directory = Path.GetDirectoryName(filePath);

//    // Ensure the directory exists
//    if (!Directory.Exists(directory))
//    {
//        Directory.CreateDirectory(directory);
//    }

//    using var writer = new StreamWriter(filePath);
//    using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
//    csv.WriteRecords(catalog);
//}
