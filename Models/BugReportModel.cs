namespace dbapp.Models;

public class BugReportModel
{
    public string Message { get; set; } = string.Empty;
    public string ProductName { get; set; }= string.Empty;
    public string CompanyName { get; set; }= string.Empty;
    public decimal VersionID { get; set; } 
}