namespace dbapp.Models;

public class GetBugReportViewModel {
    public string ProductName { get; set; }=string.Empty;
    public string VersionID { get; set; }=string.Empty;
    public string Message { get; set; }=string.Empty;
    public string CompanyName { get; set; }=string.Empty;
    public DateTime FeedbackDate { get; set; }
}