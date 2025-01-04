namespace dbapp.Models;

public class ProductInfoViewModel
{
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public DateTime ReleaseDate { get; set; }
    public decimal? VersionID { get; set; }
    public DateTime? VersionDate { get; set; }
    public string VersionDescription { get; set; }
}