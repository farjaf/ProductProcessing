using System.Globalization;
using CsvHelper;
using ProductProcessing.Models;
using ProductProcessing.Services.Interfaces;
using System.IO.Abstractions;

namespace ProductProcessing.Services.Implementation;

public class CsvExporter(IFileSystem fileSystem) : ICsvExporter
{
    public void ExportToCsv(List<Product> catalog, string filePath)
    {
        var directory = fileSystem.Path.GetDirectoryName(filePath);

        // Ensure the directory exists
        if (!fileSystem.Directory.Exists(directory))
        {
            fileSystem.Directory.CreateDirectory(directory);
        }

        using var writer = fileSystem.File.CreateText(filePath);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.WriteRecords(catalog);
    }
}