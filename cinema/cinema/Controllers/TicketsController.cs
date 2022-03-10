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

        private readonly IPaymentAdapter _paymentAdapter;
        

        public TicketsController(IPriceCalculatingService priceCalculatingService, ITicketService ticketService, IPaymentAdapter paymentAdapter)
        {
            _ticketService = ticketService;
            _paymentAdapter = paymentAdapter;
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
            [FromQuery] int show,
            [FromQuery] int quantity,
            [FromQuery] bool childDiscount,
            [FromQuery] bool studentDiscount,
            [FromQuery] bool seniorDiscount,
            [FromQuery] bool popcorn)
        {
            // ViewBag.show = ;
            ViewBag.quantity = quantity;
            ViewBag.childDiscount = childDiscount;
            ViewBag.studentDiscount = studentDiscount;
            ViewBag.seniorDiscount = seniorDiscount;
            ViewBag.popcorn = popcorn;
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