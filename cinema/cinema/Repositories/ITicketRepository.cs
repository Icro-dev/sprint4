using cinema.Models;

namespace cinema.Repositories;

public interface ITicketRepository
{
    public IQueryable<Ticket> FindTicketsByShow(Show show);

    public List<Ticket> FindShowTicketsByRow(Show show, int row);

    public List<Ticket> FindAllTickets();

    public void AddRangeTickets(List<Ticket> tickets);

    public void UpdateRangeTickets(List<Ticket> tickets);

    public void SaveTickets();
    
    

}