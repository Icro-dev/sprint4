using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cinema.Models;

public class Ticket
{
    [Key]
    public int Id { get; set; }
    public Show show { get; set; }
    public int SeatRow { get; set; }
    public int SeatNr { get; set; }
    public bool ChildDiscount { get; set; }
    public bool StudentDiscount { get; set; }
    public bool SeniorDiscount { get; set; }
    public int Code { get; set; }
    public bool CodeUsed { get; set; }
    public bool Popcorn { get; set; }
}