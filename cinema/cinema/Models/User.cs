using System.ComponentModel.DataAnnotations;

namespace cinema.Models;

public class User
{
    public int ID { get; set; }

    [Required]
    [StringLength(60, MinimumLength = 3)]
    public string Name { get; set; }
}