using System.Globalization;
using CsvHelper;
using ProductProcessing.Models;
using ProductProcessing.Services.Interfaces;
using System.IO.Abstractions;

namespace ProductProcessing.Services.Implementation;

public class CsvExporter : ICsvExporter
{
    private readonly IFileSystem _fileSystem;

    public CsvExporter(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public void ExportToCsv(List<Product> catalog, string filePath)
    {
        var directory = _fileSystem.Path.GetDirectoryName(filePath);

        // Ensure the directory exists
        if (!_fileSystem.Directory.Exists(directory))
        {
            _fileSystem.Directory.CreateDirectory(directory);
        }

        using var writer = _fileSystem.File.CreateText(filePath);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.WriteRecords(catalog);
    }
}