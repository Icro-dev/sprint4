#nullable disable
using Microsoft.AspNetCore.Mvc;
using cinema.Models;
using cinema.Services;

namespace cinema.Controllers
{
    public class TicketsController : Controller
    {

        private readonly ITicketService _ticketService;

        private readonly IPriceCalculatingService _priceCalculatingService;

        public TicketsController(IPriceCalculatingService priceCalculatingService, ITicketService ticketService)
        {
            _ticketService = ticketService;
            _priceCalculatingService = priceCalculatingService;
        }

        [HttpGet]
        [Route("/tickets")]
        [Route("/tickets/index")]
        public IActionResult Index()
        {
            return View(_ticketService.GetAllTickets());
        }

        [HttpGet]
        [Route("/tickets/quantity")]
        public IActionResult Quantity()
        {
            return View();
        }

        [HttpGet]
        [Route("/tickets/create")]
        public IActionResult Create([FromQuery] int show, [FromQuery] int quantity)
        {
            ViewBag.show = show;
            ViewBag.price = _priceCalculatingService.pricePerTicket();
            ViewBag.totalPrice = _priceCalculatingService.totalPrice(quantity);
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

        // private bool TicketExists(int id)
        // {
        //     return _context.Tickets.Any(e => e.Id == id);
        // }
    }
}