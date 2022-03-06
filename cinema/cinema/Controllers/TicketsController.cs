#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cinema.Data;
using cinema.Models;

namespace cinema.Controllers
{
    public class TicketsController : Controller
    {
        private readonly CinemaContext _context;

        public TicketsController(CinemaContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/tickets")]
        [Route("/tickets/index")]
        public IActionResult Index()
        {
            IEnumerable<Ticket> tickets = _context.Tickets;
            return View(tickets);
        }

        [HttpGet]
        [Route("/tickets/quantity")]
        public IActionResult Quantity()
        {
            return View();
        }

        [HttpGet]
        [Route("/tickets/create")]
        public IActionResult Create([FromQuery] string show,[FromQuery] string quantity)
        {
            ViewBag.show = show;
            ViewBag.quantity = quantity;
            return View();
        }

        [HttpPost]
        [Route("/tickets/create")]
        public IActionResult Create([FromQuery] int show, [FromQuery] int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                Ticket ticket = new Ticket();
                ticket.SeatRow = 2;
                ticket.SeatNr = 3;
                
                ticket.ChildDiscount = true;
                ticket.SeniorDiscount = true;
                ticket.StudentDiscount = true;
                ticket.Popcorn = true;
                
                ticket.show = _context.Shows.Find(show);
                ticket.Code = new Random().Next(1, 100000);
                ticket.CodeUsed = false;
                _context.Tickets.Add(ticket);
                _context.SaveChanges();
            }
         
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}