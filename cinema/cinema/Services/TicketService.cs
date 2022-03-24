using System.Collections;
using cinema.Data;
using cinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cinema.Services;

public class TicketService : ITicketService
{
    private readonly CinemaContext _context;
    private readonly ISeatService _seatService;
    public TicketService(CinemaContext context, ISeatService seatService)
    {
        _context = context;
        _seatService = seatService;
    }

    public IEnumerable<Ticket>? GetAllTickets()
    {
        return _context.Tickets;
    }
    
    public List<Ticket> CreateTickets(
        int show,
        int quantity,
        int childDiscount,
        int seniorDiscount,
        int studentDiscount,
        int popcorn,
        Arrangements arrangements
    )
    {
        if (_context.Shows == null || !_context.Shows.Any() )throw new Exception();
        var theShow = _context.Shows.First(s => s.Id == show);
        var theSeats = _seatService.GetSeats(theShow, quantity);


        var tickets = new List<Ticket>();
        
        for (int i = 0; i < quantity; i++)
        {
            Ticket ticket = new Ticket();
            ticket.SeatRow = theSeats[i,0];
            ticket.SeatNr = theSeats[i,1];
                
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

            ticket.show = _context.Shows.Include(s => s.Movie).Where(s => s.Id == show).First();
            ticket.Code = new Random().Next(1, 100000);
            ticket.CodeUsed = false;
            ticket.Arrangements = arrangements;
            tickets.Add(ticket);
        }
       
        _context.Tickets.AddRange(tickets);
        _context.SaveChanges();
        return tickets;
    }
}
