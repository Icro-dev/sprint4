using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cinema.Models;

public class Show
{
    [Key]
    public int Id { get; set; }
    public bool ThreeD { get; set; }
    public int Room { get; set; }
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime StartTime { get; set; }
    public bool Break { get; set; }
    public Movie Movie { get; set; }    
}