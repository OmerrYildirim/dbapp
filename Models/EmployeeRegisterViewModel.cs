using System.ComponentModel.DataAnnotations;

namespace dbapp.Models;

public class EmployeeRegisterViewModel {
    [Required] public string FullName { get; set; } 
    [Required] public string UserEmail { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
    
}