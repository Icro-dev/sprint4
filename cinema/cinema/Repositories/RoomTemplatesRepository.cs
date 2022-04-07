using System.Linq;
using cinema.Data;
using cinema.Models;
using Microsoft.AspNetCore.Mvc;
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
    
    public Task<RoomTemplate?> FindRoomTemplatesById(int id)
    {
        return _context.RoomTemplates
            .FirstOrDefaultAsync(m => m.Id == id);
    }
    
   
    public void Add( RoomTemplate roomTemplate)
    {
        _context.Add(roomTemplate);
    }

    public void SaveRoomTemplate()
    { 
        _context.SaveChangesAsync();
    }
    
    public void UpdateRoomTemplate(RoomTemplate roomTemplate)
    {
        _context.Update(roomTemplate);
    }
    
    public void RemoveRoomTemplate(RoomTemplate roomTemplate)
    {
        _context.RoomTemplates.Remove(roomTemplate);
    }

    public bool RoomTemplateExists(int id)
    {
        return _context.RoomTemplates.Any(e => e.Id == id);
    }
}
