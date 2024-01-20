using CsvHelper;
using ProductProcessing.Services.Interfaces;
using System.Globalization;

namespace ProductProcessing.Services.Implementation;

public class CsvReaderService : ICsvReaderService
{
    public IEnumerable<T> ReadCsvFile<T>(string filePath)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        
        return csv.GetRecords<T>().ToList();
    }
}