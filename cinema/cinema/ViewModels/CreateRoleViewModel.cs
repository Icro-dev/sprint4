using System.ComponentModel.DataAnnotations;

namespace cinema.ViewModels;

public class CreateRoleViewModel
{
    [Required]
    public string RoleName { get; set; }
}