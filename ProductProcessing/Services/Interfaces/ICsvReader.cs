namespace ProductProcessing.Services.Interfaces;

public interface ICsvReaderService
{
    IEnumerable<T> ReadCsvFile<T>(string filePath);
}