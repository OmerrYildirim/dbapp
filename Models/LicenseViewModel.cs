namespace dbapp.Models;

public class LicenseViewModel
{
    public string ProductName { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalFee { get; set; }
}