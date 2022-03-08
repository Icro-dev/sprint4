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

    public string GetSeats(Show show, int quantity)
    {
        if (show == null || !(quantity > 0) )
        {
            throw new MissingFieldException();
        }

        string template = null;
        if (_context.RoomTemplates != null && _context.RoomTemplates.Any())
        {
            template = _context.RoomTemplates.First(t => t.Id == 1).Setting;
        }

        string[] response = new string[quantity];

        var tickets = from t in _context.Tickets
            select t;
        tickets = tickets.Where(t => t.show.Equals(show));

        if (template != null)
        {
            return template;
        }
        else return "Not Found";
    }
}