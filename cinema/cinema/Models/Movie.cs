using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cinema.Models;

public class Movie
{
    [Key]
    public string Name { get; set; }
    public string Description { get; set; }
    public double Length { get; set; }
    public string Genre { get; set; }
    public int AdvisedAge { get; set; }
    public string Poster { get; set; }
    public string Language { get; set; }
    public bool ThreeD { get; set; }
}