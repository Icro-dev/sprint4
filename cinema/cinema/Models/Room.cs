using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cinema.Models;

public class Room
{
    [Key]
    public int Id { get; set; }
    public int RoomNr { get; set; }
    public RoomTemplate Template { get; set; }
    public bool Wheelchair { get; set; }
    public bool ThreeD { get; set; }
    public Theatre Theatre { get; set; }
}