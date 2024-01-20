using ProductProcessing.Models;

namespace ProductProcessing.Services.Interfaces;

public interface ICatalogMerger
{
    List<Product> MergeCatalogs(List<Product> catalogA, List<Product> catalogB);
}