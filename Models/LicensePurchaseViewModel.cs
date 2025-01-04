namespace dbapp.Models;

public class LicensePurchaseViewModel {
    public int ProductID { get; set; }
    public int CompanyID { get; set; }
    public int LicenseTerm { get; set; } // License term in days
}