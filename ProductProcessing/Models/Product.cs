namespace ProductProcessing.Models;

public class Product
{
    public string SKU { get; set; }
    public string Description { get; set; }
    public string Source { get; set; }

    public List<string> Barcodes { get; set; }
}