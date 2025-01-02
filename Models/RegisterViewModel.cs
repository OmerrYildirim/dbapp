using System.ComponentModel.DataAnnotations;

namespace dbapp.Models;

public class RegisterViewModel {
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string Surname { get; set; } = string.Empty;
    [Required] public string UserEmail { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
    [Required]public string UserType { get; set; } = string.Empty;
    
}