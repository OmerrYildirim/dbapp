using System.ComponentModel.DataAnnotations;

namespace dbapp.Models;

public class LoginViewModel {
    [Required] public string UserEmail { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
    [Required]public string UserType { get; set; } = string.Empty;
}