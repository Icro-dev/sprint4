using cinema.Data;
using cinema.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace cinema.Services
{
    public class RoomService : IRoomService
    {
        CinemaContext _context;

        public RoomService(CinemaContext context)
        {
            _context = context;
        }

        public int[] GetRoomTemplate(Room room)
        {
            return JsonSerializer.Deserialize<int[]>(room.Template.Setting);
        }

        public Room GetShowRoom(Show show)
        {
            return _context.Rooms.Include(r => r.Template).Where(r => r.Id == show.Room).First();
        }
    }
}
