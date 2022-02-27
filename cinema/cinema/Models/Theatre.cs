using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cinema.Models;

public class Theatre
{
    [Key]
    public int Id { get; set; }
    public int Location { get; set; }
}