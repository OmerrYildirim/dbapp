namespace dbapp.Models;

public class FeatureRequestModel
{
    public string Message { get; set; }= string.Empty;
    public string ProductName { get; set; }= string.Empty;
    public string CompanyName { get; set; }= string.Empty;
    public int Rating { get; set; }
}