namespace ProductProcessing.Models;

public class Product
{
    public string SKU { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Source { get; set; } = string.Empty;

    public List<string> Barcodes { get; set; } = [];
}