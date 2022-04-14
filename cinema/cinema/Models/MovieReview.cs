using System.ComponentModel.DataAnnotations;

namespace cinema.Models
{
    public class MovieReview
    {
        [Key]
        public int Id { get; set; }
        public string? NameOfMovie { get; set; }
        public string? UserName { get; set; }
        public string? Review { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime PostTime { get; set; }
        
    }
}
