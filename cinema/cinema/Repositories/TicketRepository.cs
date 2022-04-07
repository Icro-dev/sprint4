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

    public List<Ticket> FindAllTickets()
    {
        return new List<Ticket>(_context.Tickets);
    }
    
    public IQueryable<Ticket> FindTicketsByShow(Show show)
    {
        return (IQueryable<Ticket>) _context.Tickets.Where(t => t.show.Equals(show));
    }

    public List<Ticket> FindShowTicketsByRow(Show show, int row)
    {
        return _context.Tickets.Where(t => t.show.Equals(show)).Where(t => t.SeatRow.Equals(row + 1)).ToList();
    }

    public void AddRangeTickets(List<Ticket> tickets)
    {
        _context.Tickets.AddRange(tickets);
    }

    public void UpdateRangeTickets(List<Ticket> tickets)
    {
        _context.Tickets.UpdateRange(tickets);
    }

    public void SaveTickets()
    {
        _context.SaveChanges();
    }
}