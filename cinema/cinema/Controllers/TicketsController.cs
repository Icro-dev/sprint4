#nullable disable
using System.Linq;
using cinema.Data;
using Microsoft.AspNetCore.Mvc;
using cinema.Models;
using cinema.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
        public IActionResult Index(string serializedTickets)
        {
            var tickets = JsonConvert.DeserializeObject<List<Ticket>>(serializedTickets);
            return View(tickets);
        }

         
        [HttpGet]
        [Route("/tickets/arrangements")]
        public IActionResult Arrangements([FromQuery] int showId)
        {
            Movie movie = _movieService.GetMovieFromShow(showId);
            ViewBag.Movie = movie;
            ViewBag.ShowId = showId;
            ViewBag.Vip = cinema.Arrangements.vip;
            ViewBag.Normal = cinema.Arrangements.normale;
            ViewBag.ChildrenParty = cinema.Arrangements.kinderfeestje;
            return View();
        }
        
        [HttpGet]
        [Route("/tickets/quantity")]
        public IActionResult Quantity([FromQuery] int showId,[FromQuery] Arrangements arrangement)
        {
            Movie movie = _movieService.GetMovieFromShow(showId);
            ViewBag.Movie = movie;
            ViewBag.ShowId = showId;
            ViewBag.Arrangement = arrangement;
            return View();
        }

       
        [HttpGet]
        [Route("/tickets/create")]
        public IActionResult Create(
            [FromQuery] int showId,
            [FromQuery] int quantity,
            [FromQuery] Arrangements arrangement)
        {
            ViewBag.show = showId;
            ViewBag.price = _priceCalculatingService.pricePerTicket(showId);
            ViewBag.totalPrice = _priceCalculatingService.ticketCost(quantity, showId);
            ViewBag.quantity = quantity;
            ViewBag.Arrangement = arrangement;
            return View();
        }
        
        [HttpGet]
        [Route("/tickets/reservation")]
        public IActionResult Reservation(
            [FromQuery] int showId,
            [FromQuery] int quantity,
            [FromQuery] int childDiscount,
            [FromQuery] int studentDiscount,
            [FromQuery] int seniorDiscount,
            [FromQuery] int popcorn,
            [FromQuery] Arrangements arrangement)
        
        {
            
            ViewBag.quantity = quantity;
            ViewBag.showId = showId;
            ViewBag.childDiscount = childDiscount;
            ViewBag.studentDiscount = studentDiscount;
            ViewBag.seniorDiscount = seniorDiscount;
            ViewBag.popcorn = popcorn;
            
            ViewBag.Arrangement = arrangement;
            
            var ticketCost = _priceCalculatingService.ticketCost(quantity, showId);
            var discount = _priceCalculatingService.Discount(childDiscount, studentDiscount, seniorDiscount);
            var premium = _priceCalculatingService.Premium(popcorn);

            ViewBag.TicketCost = ticketCost;
            ViewBag.Discount = discount;
            ViewBag.Popcorn = premium;
            ViewBag.OrderCost = _priceCalculatingService.OrderCost(discount, premium, ticketCost);
            return View();
        }

        [HttpPost]
        [Route("/tickets/create")]
        public IActionResult Create(
            [FromForm] int showId,
            [FromForm] int quantity,
            [FromForm] int childDiscount,
            [FromForm] int seniorDiscount,
            [FromForm] int studentDiscount,
            [FromForm] int popcorn,
            [FromForm] Arrangements arrangement)
        {
           var tickets =  _ticketService.CreateTickets(
                showId,
                quantity,
                childDiscount,
                seniorDiscount,
                studentDiscount,
                popcorn,
                arrangement);

           var myTicketsId = new List<int>();
           foreach (var ticket in tickets)
           {
               myTicketsId.Add(ticket.Id);
           }
           TempData["myTickets"] = myTicketsId;

            return RedirectToAction("Index", new
            {
                serializedTickets = JsonConvert.SerializeObject(tickets.ToList()) 
            });
        }
    }
}