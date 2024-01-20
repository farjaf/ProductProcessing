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


