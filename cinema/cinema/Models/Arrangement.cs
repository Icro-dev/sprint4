using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cinema.Models;

public class Arrangement
{
    [Key]
    public int Id { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
}