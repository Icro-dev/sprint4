using System.Linq;
using cinema.Data;
using cinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace cinema.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly CinemaContext _context;

        public PaymentRepository(CinemaContext cinemaContext)
        {
            _context = cinemaContext;
        }

        public TicketOrder GetFirstOrder([FromForm] int orderid)
        {
           return _context.Orders.First(o => o.Id == orderid);
        }
        
        public Show GetFirstShow([FromForm] int orderid)
        {
            var order = GetFirstOrder(orderid);
            return _context.Shows.Include(s => s.Movie).First(s => s.Id == order.ShowId);
        }

        public void GetUpdatePaymenttOrderSave([FromQuery] int id)
        {
            var order = GetFirstOrder(id);
            _context.Orders.First(o => o.Id == id);
            order.IsPayed = true;
            _context.Orders.Update(order);
            _context.SaveChanges();
        }
        

        
}