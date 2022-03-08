using System.Collections;
using cinema.Data;
using cinema.Models;

namespace cinema.Services;

public class TicketService : ITicketService
{
    private readonly CinemaContext _context;

    public TicketService(CinemaContext context)
    {
        _context = context;
    }

    public IEnumerable<Ticket>? GetAllTickets()
    {
        return _context.Tickets;
    }
    
    public void CreateTickets(
        int show,
        int quantity,
        int childDiscount,
        int seniorDiscount,
        int studentDiscount,
        int popcorn
    )
    {
        for (int i = 0; i < quantity; i++)
        {
            Ticket ticket = new Ticket();
            ticket.SeatRow = 2;
            ticket.SeatNr = 3;
                
            if(childDiscount >= (i + 1))
            {
                ticket.ChildDiscount = true;
            }

            if (seniorDiscount >= (i + 1))
            {
                ticket.SeniorDiscount = true;
            }

            if (studentDiscount >= (i + 1))
            {
                ticket.StudentDiscount = true;
            }

            if (popcorn >= (i + 1))
            {
                ticket.Popcorn = true;
            }

            ticket.show = _context.Shows.Find(show);
            ticket.Code = new Random().Next(1, 100000);
            ticket.CodeUsed = false;
            _context.Tickets.Add(ticket);
            _context.SaveChanges();
        }
    }
}
