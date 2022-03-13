#nullable disable
using System.Linq;
using cinema.Data;
using Microsoft.AspNetCore.Mvc;
using cinema.Models;
using cinema.Services;
using Microsoft.EntityFrameworkCore;

namespace cinema.Controllers
{

   
    public class TicketsController : Controller
    {
        private readonly CinemaContext _context;

        private readonly ITicketService _ticketService;

        private readonly IPriceCalculatingService _priceCalculatingService;

        private readonly IPaymentAdapter _paymentAdapter;

        private readonly IMovieService _movieService;
        

        public TicketsController(IPriceCalculatingService priceCalculatingService, ITicketService ticketService, IPaymentAdapter paymentAdapter, IMovieService movieService, CinemaContext context)
        {
            _ticketService = ticketService;
            _paymentAdapter = paymentAdapter;
            _movieService = movieService;
            _context = context;
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
        public IActionResult Create([FromQuery] int showId, [FromQuery] int quantity)
        {
            ViewBag.show = showId;
            ViewBag.price = _priceCalculatingService.pricePerTicket(showId);
            ViewBag.totalPrice = _priceCalculatingService.totalPrice(showId, quantity);
            ViewBag.quantity = quantity;
            return View();
        }
        
        [HttpGet]
        [Route("/tickets/payment")]
        public IActionResult Payment(
            [FromQuery] int showId,
            [FromQuery] int quantity,
            [FromQuery] bool childDiscount,
            [FromQuery] bool studentDiscount,
            [FromQuery] bool seniorDiscount,
            [FromQuery] bool popcorn)
        {
            
            ViewBag.quantity = quantity;
            return View();
        }

        [HttpPost]
        [Route("/tickets/create")]
        public IActionResult Create(
            [FromQuery] int show,
            [FromQuery] int quantity,
            [FromQuery] int childDiscount,
            [FromQuery] int seniorDiscount,
            [FromQuery] int studentDiscount,
            [FromQuery] int popcorn)
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