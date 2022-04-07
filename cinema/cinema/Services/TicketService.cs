using cinema.Models;
using cinema.Repositories;

namespace cinema.Services;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IShowRepository _showRepository;
    private readonly ISeatService _seatService;

    public TicketService(ISeatService seatService, ITicketRepository ticketRepository, IShowRepository showRepository)
    {
        _seatService = seatService;
        _ticketRepository = ticketRepository;
        _showRepository = showRepository;
    }

    public IEnumerable<Ticket>? GetAllTickets()
    {
        return _ticketRepository.FindAllTickets();
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
        var theShow = _showRepository.FindShowByIdIncludeMovie(show);
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

            ticket.show = theShow;
            ticket.Code = new Random().Next(1, 100000);
            ticket.CodeUsed = false;
            ticket.Arrangements = arrangements;
            tickets.Add(ticket);
        }
        _ticketRepository.AddRangeTickets(tickets);
        _ticketRepository.SaveTickets();
        return tickets;
    }

    public void PushTickets(List<Ticket> tickets)
    {
        foreach (Ticket ticket in tickets)
        {
            ticket.show = _showRepository.FindShowByIdIncludeMovie(ticket.show.Id);
            
        }
        _ticketRepository.UpdateRangeTickets(tickets);
        _ticketRepository.SaveTickets();
    }
}
