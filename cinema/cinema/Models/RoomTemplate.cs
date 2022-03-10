using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace cinema.Models;

public class RoomTemplate
{
    public RoomTemplate(string setting)
    {
        Setting = setting;
        try
        {
            int[] settingArray = JsonSerializer.Deserialize<int[]>(Setting);
            for (int i = 0; i < settingArray.Length; i++)
            {
                Capacity +=  settingArray[i];
            }
            
        }
        catch (Exception)
        {
            throw new ArgumentException("Setting is not correcty formatted");
        }
    }
    [Key] public int Id { get; set; }
    public string Setting { get; set; }

    public int Capacity { get; set; }

}