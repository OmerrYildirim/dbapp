namespace dbapp.Models;

public class GetFeatureRequestViewModel {
    public string ProductName { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string Message { get; set; }= string.Empty;
    public string CompanyName { get; set; }= string.Empty;
    public DateTime FeedbackDate { get; set; }
}