using cinema.Data;
using cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace cinema.Repositories;

public class TicketRepository : ITicketRepository
{
    
    private readonly CinemaContext _context;

    public TicketRepository(CinemaContext cinemaContext)
    {
        _context = cinemaContext;
    }
    
    public IQueryable<Ticket> FindTicketsByShow(Show show)
    {
        return _context.Tickets.Where(t => t.show.Equals(show));
    }

    public List<Ticket> FindShowTicketsByRow(Show show, int row)
    {
        return _context.Tickets.Where(t => t.show.Equals(show)).Where(t => t.SeatRow.Equals(row + 1)).ToList();
    }
}