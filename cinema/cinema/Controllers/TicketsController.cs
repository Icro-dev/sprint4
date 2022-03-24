#nullable disable
using System.Linq;
using cinema.Data;
using Microsoft.AspNetCore.Mvc;
using cinema.Models;
using cinema.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System.Text.Json;
using Stripe;

namespace cinema.Controllers
{
    public class TicketsController : Controller
    {
        private readonly CinemaContext _context;

        private readonly ITicketService _ticketService;

        private readonly IPriceCalculatingService _priceCalculatingService;

        private readonly IShowService _showService;

        private readonly IMovieService _movieService;

        private readonly IConfiguration _config;


        public TicketsController(IPriceCalculatingService priceCalculatingService, ITicketService ticketService,
            IMovieService movieService, CinemaContext context, IShowService showService, IConfiguration config)
        {
            _ticketService = ticketService;
            _movieService = movieService;
            _context = context;
            _showService = showService;
            _config = config;
            _priceCalculatingService = priceCalculatingService;
        }

        [HttpGet]
        [Route("/tickets")]
        [Route("/tickets/index")]
        public IActionResult Index(int id)
        {
            ViewBag.StripePublishKey = _config["StripePubKey"];
            var order = _context.Orders.First(o => o.Id == id);
            var ticketIdsString = order.SerializedTicketIds;
            var ticketIds =  JsonSerializer.Deserialize<int[]>(ticketIdsString);
            var tickets = new List<Ticket>();
            foreach (var ticketId in ticketIds)
            {
                tickets.Add(_context.Tickets.Include(t => t.show.Movie).First(t => t.Id == ticketId));
            }

            ViewBag.Cost = order.Cost;
            ViewBag.OrderId = id;
            ViewBag.IsPayed = order.IsPayed;
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
        public IActionResult Quantity([FromQuery] int showId, [FromQuery] Arrangements arrangement)
        {
            Movie movie = _movieService.GetMovieFromShow(showId);
            ViewBag.Movie = movie;
            ViewBag.ShowId = showId;
            ViewBag.Arrangement = arrangement;
            return View();
        }


        [HttpGet]
        [Route("/tickets/create")]
        public IActionResult Discount(
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
            ViewBag.movieName = _showService.getShowById(showId).Movie.Name;
            ViewBag.threeD = _showService.getShowById(showId).ThreeD;
            ViewBag.childDiscount = childDiscount;
            ViewBag.studentDiscount = studentDiscount;
            ViewBag.seniorDiscount = seniorDiscount;
            ViewBag.popcorn = popcorn;


            ViewBag.Arrangement = arrangement;

            var totalCost = _priceCalculatingService.ticketCost(quantity, showId);
            var discount = _priceCalculatingService.Discount(childDiscount, studentDiscount, seniorDiscount);
            var premium = _priceCalculatingService.Premium(popcorn);
            var arrangementCost = _priceCalculatingService.ArrangementCost(arrangement);

            ViewBag.arrangementCost = arrangementCost;
            ViewBag.totalCost = totalCost;
            ViewBag.Discount = discount;
            ViewBag.Popcorn = premium;
            ViewBag.OrderCost = _priceCalculatingService.OrderCost(discount, premium, totalCost, arrangementCost);
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
            [FromForm] Arrangements arrangement,
            [FromForm] double orderCost
        )
        {
            var tickets = _ticketService.CreateTickets(
                showId,
                quantity,
                childDiscount,
                seniorDiscount,
                studentDiscount,
                popcorn,
                arrangement
            );
            var myTicketsId = new List<int>();
            foreach (var ticket in tickets)
            {
                myTicketsId.Add(ticket.Id);
            }

            var order = new TicketOrder()
            {
                Cost = orderCost,
                SerializedTicketIds = JsonSerializer.Serialize(myTicketsId),
                ShowId = showId
            };
            _context.Orders.Add(order);
            _context.SaveChanges();


            TempData["myTickets"] = myTicketsId;

            return RedirectToAction("Index", new
            {
                id = order.Id
            });
        }

        [HttpPost]
        [Route("/tickets/betalen")]
        public IActionResult Payment(TicketOrder order)
        {
            ViewBag.order = order;
            ViewBag.StripePubKey = _config["StripePubKey"];
            return View();
        }

        [HttpPost]
        public void Pay(string stripeToken, TicketOrder order)
        {
            Stripe.StripeConfiguration.SetApiKey(_config["StripePubKey"]);
            Stripe.StripeConfiguration.ApiKey = _config["StripeKey"];

            var myCharge = new Stripe.ChargeCreateOptions();
            // always set these properties
            myCharge.Amount = (long?) (order.Cost * 100);
            myCharge.Currency = "EUR";
            myCharge.Description = order.Id.ToString();
            myCharge.Source = stripeToken;
            myCharge.Capture = true;
            var chargeService = new Stripe.ChargeService();
            Charge stripeCharge = chargeService.Create(myCharge);
            Console.WriteLine(JsonSerializer.Serialize(stripeCharge));

            RedirectToAction("Create");
        }
    }
}