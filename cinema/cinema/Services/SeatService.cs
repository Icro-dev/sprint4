using System.Linq;
using cinema.Data;
using cinema.Models;

namespace cinema.Services;

public class SeatService : ISeatService
{
    private readonly CinemaContext _context;

    public SeatService(CinemaContext context)
    {
        _context = context;
    }

    public Array GetSeats(Show show, int quantity)
    {
        if (show == null || !(quantity > 0) )
        {
            throw new MissingFieldException();
        }

        var template = from t in _context.RoomTemplates
            where t.Id == show.Room.Template.Id
            select t;
        
        string[] response = new string[quantity];

        var tickets = from t in _context.Tickets
            select t;
        tickets = tickets.Where(t => t.show.Id.Equals(showId));
        
        
        return response;
    }
}