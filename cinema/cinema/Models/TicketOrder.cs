using System.ComponentModel.DataAnnotations;

namespace cinema.Models;

public class TicketOrder
{

    [Key] 
    public int Id { get; set; }
    public string SerializedTicketIds { get; set; }
    public double Cost { get; set; }
    public bool IsPayed {get; set; }
    public int ShowId { get; set; }
}