using cinema.Models;

namespace cinema.Services;

public interface ITicketService
{
    public List<Ticket> CreateTickets(  int show,
        int quantity,
        int childDiscount,
        int seniorDiscount,
        int studentDiscount,
        int popcorn);

    public IEnumerable<Ticket>? GetAllTickets();

}