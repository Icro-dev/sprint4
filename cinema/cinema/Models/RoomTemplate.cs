using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cinema.Models;

public class RoomTemplate
{
    public RoomTemplate(string setting)
    {
        Setting = setting;
    }
    
    public RoomTemplate()
    {
    }

    [Key]
    public int Id { get; set; }
    public string Setting { get; set; }    
}