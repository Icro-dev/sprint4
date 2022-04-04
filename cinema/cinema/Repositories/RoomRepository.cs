using System.Linq;
using cinema.Data;
using cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace cinema.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly CinemaContext _context;

    public RoomRepository(CinemaContext context)
    {
        _context = context;
    }

    public Room findRoomByShow(Show show)
    {
        return _context.Rooms.Include(r => r.Template).First(r => r.Id == show.Room);
    }
    
}