using System.ComponentModel.DataAnnotations;

namespace cinema.Models
{
    public class Abonnement
    {
        [Key]
        public int Id { get; set; }
        public string? AbboQR { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ExpireDate { get; set; }

        public string? AbboName { get; set; }
        public bool Expired { get; set; }

    }
}
