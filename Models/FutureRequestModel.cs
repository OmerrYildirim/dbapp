using System.ComponentModel.DataAnnotations;

namespace dbapp.Models;

public class FeatureRequestModel
{
    public string Message { get; set; }= string.Empty;
    public string ProductName { get; set; }= string.Empty;
    
    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
    public int Rating { get; set; }
}