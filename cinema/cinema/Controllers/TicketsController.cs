#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cinema.Data;
using cinema.Models;
using cinema.Services;

namespace cinema.Controllers
{
    public class TicketsController : Controller
    {
        private readonly CinemaContext _context;

        private readonly ITicketService _ticketService;
        
        public TicketsController(CinemaContext context, ITicketService ticketService)
        {
            _context = context;
            _ticketService = ticketService;
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
        public IActionResult Create([FromQuery] string show, [FromQuery] string quantity)
        {
            ViewBag.show = show;
            ViewBag.quantity = quantity;
            return View();
        }

        [HttpPost]
        [Route("/tickets/create")]
        public IActionResult Create(
            [FromQuery] int show,
            [FromQuery] int quantity,
            [FromForm] int childDiscount,
            [FromForm] int seniorDiscount,
            [FromForm] int studentDiscount,
            [FromForm] int popcorn)
        {
            _ticketService.CreateTickets(
                show,
                quantity,
                childDiscount,
                seniorDiscount,
                studentDiscount,
                popcorn);
            
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}