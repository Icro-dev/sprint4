using System.ComponentModel.DataAnnotations;

namespace cinema.Models;

public class MovieReview
{
    [Key]
    public int Id { get; set; }
    
    public string UserName{ get; set; }
    
    public string UserReview{ get; set; }
    
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime PostDateTime { get; set; }
    
    public string MovieName { get; set; } 
}