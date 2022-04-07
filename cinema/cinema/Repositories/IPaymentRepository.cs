using cinema.Models;
using Microsoft.AspNetCore.Mvc;

namespace cinema.Repositories;

public interface IPaymentRepository
{
    public TicketOrder GetFirstOrder([FromForm] int orderid);

    public Show GetFirstShow([FromForm] int orderid);

    public void GetUpdatePaymenttOrderSave([FromQuery] int id);
}