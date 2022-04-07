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
    
    public async Task<List<Room>> ListOfAllRooms()
    {
        return await _context.Rooms.ToListAsync();
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