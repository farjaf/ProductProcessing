using ProductProcessing.Models;

namespace ProductProcessing.Services.Interfaces;

public interface ICsvExporter
{
    void ExportToCsv(List<Product> catalog, string filePath);
}