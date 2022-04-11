using System.ComponentModel.DataAnnotations;

namespace cinema.ViewModels;

public class RegisterUserViewModel
{
    [Required]
    public string Name { get; set; }

    [Required, EmailAddress]
    [UIHint("email")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [Display(Name = "Confirm password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Invalid password")]
    public string ConfirmedPassword { get; set; }
}