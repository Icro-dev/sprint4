using System.ComponentModel.DataAnnotations;

namespace cinema.Models
{
    public class LostAndFound
    {
        [Key]
        public int Id { get; set; }
        public string? LostObject { get; set; }
        public string? Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FoundTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime RetrievedTime { get; set; }
    }
}
