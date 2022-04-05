using System.Linq;
using cinema.Data;
using cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace cinema.Repositories;

public class RoomTemplatesRepository : IRoomTemplatesRepository
{
    
    private readonly CinemaContext _context;

    public RoomTemplatesRepository(CinemaContext context)
    {
        _context = context;
    }

    public async Task<List<RoomTemplate>> ListOfAllRoomTemplates()
    {
        return await _context.RoomTemplates.ToListAsync();
    }
    
    public Task<Room?> FindRoomById(int id)
    {
        return _context.Rooms
            .FirstOrDefaultAsync(m => m.Id == id);
    }
    
    public void Add(Room room)
    {
        _context.Add(room);
    }

    public void SaveRoom()
    { 
        _context.SaveChangesAsync();
    }
    
    public void UpdateRoom(Room room)
    {
        _context.Update(room);
    }
    
    public void RemoveRoom(Room room)
    {
        _context.Rooms.Remove(room);
    }

    public bool RoomExists(int id)
    {
        return _context.Rooms.Any(e => e.Id == id);
    }
}
