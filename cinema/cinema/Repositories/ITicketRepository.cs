using cinema.Models;

namespace cinema.Repositories;

public interface ITicketRepository
{
    public IQueryable<Ticket> FindTicketsByShow(Show show);

    public List<Ticket> FindShowTicketsByRow(Show show, int row);


}