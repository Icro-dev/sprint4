using System.ComponentModel.DataAnnotations;

namespace cinema.Models;

public class Subscriber
{
    [Key]
    public int Id { get; set; }
    
    public string Email { get; set; }
}