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
using cinema.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace cinema.Controllers
{

   
    public class TicketsController : Controller
    {
        private readonly CinemaContext _context;

        private readonly ITicketService _ticketService;

        private readonly IPriceCalculatingService _priceCalculatingService;

        private readonly IShowService _showService;

        private readonly IMovieService _movieService;

        private readonly IRoomService _roomService;

        private readonly IConfiguration _config;

        private readonly ISeatService _seatService;       
        public TicketsController(IPriceCalculatingService priceCalculatingService, ITicketService ticketService,
            IMovieService movieService, IRoomService roomService, ISeatService seatService, CinemaContext context, IShowService showService, IConfiguration config)
        {
            _ticketService = ticketService;
            _movieService = movieService;
            _context = context;
            _showService = showService;
            _config = config;
            _priceCalculatingService = priceCalculatingService;
            _roomService = roomService;
            _seatService = seatService;
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
            [FromQuery] Arrangements arrangement,
            [FromQuery] bool abonnement)
        {
            ViewBag.show = showId;
            ViewBag.price = _priceCalculatingService.pricePerTicket(showId);
            ViewBag.totalPrice = _priceCalculatingService.ticketCost(quantity, showId, abonnement);
            ViewBag.quantity = quantity;
            ViewBag.Arrangement = arrangement;
            ViewBag.abonnement = abonnement;
            return View();
        }

        [HttpGet]
        [Route("/tickets/seatselection")]
        public IActionResult SeatSelection([FromQuery] int orderid)
        {
            ViewData["orderid"] = orderid;
            TicketOrder order = _context.Orders.Where(o => o.Id == orderid).First();
            int[] ticketids = JsonSerializer.Deserialize<int[]>(order.SerializedTicketIds);
            List<Ticket> tickets = new List<Ticket>();
            foreach (int ticketid in ticketids)
                tickets.Add(_context.Tickets.Include(t => t.show).Where(t => t.Id == ticketid).First());
            Room room = _roomService.GetShowRoom(tickets[0].show);
            int[] roomtemplate = _roomService.GetRoomTemplate(room);
            ViewData["serializedTickets"] = JsonSerializer.Serialize(tickets.ToList());
            ViewData["roommap"] = roomtemplate;
            List<List<int>> takenseats = _seatService.GetTakenSeats(tickets[0].show);
            foreach(Ticket ticket in tickets)
            {
                if (takenseats[ticket.SeatRow - 1].Contains(ticket.SeatNr))
                {
                    takenseats[ticket.SeatRow - 1].Remove(ticket.SeatNr);
                }
            }
            ViewData["takenseats"] = takenseats;
            return View(tickets);
        }

        [HttpPost]
        [Route("/tickets/seatselection")]
        public IActionResult SeatSelection(string serializedTickets, int orderid, int seatrow, int seatnr)
        {
            ViewData["orderid"] = orderid;
            List<Ticket> tickets = JsonSerializer.Deserialize<List<Ticket>>(serializedTickets);
            int[] distances = new int[tickets.Count()];
            for (int i = 0; i < tickets.Count(); i++)
            {
                Ticket ticket = tickets[i];
                int distance = Math.Abs(ticket.SeatRow - seatrow) + Math.Abs(ticket.SeatNr - seatnr);
                distances[i] = distance;
            }
            Ticket furthest = tickets[Array.IndexOf(distances, distances.Max())];
            furthest.SeatRow = seatrow;
            furthest.SeatNr = seatnr;
            _ticketService.PushTickets(tickets);
            ViewData["serializedTickets"] = JsonSerializer.Serialize(tickets.ToList());
            Room room = _roomService.GetShowRoom(tickets[0].show);
            int[] roomtemplate = _roomService.GetRoomTemplate(room);
            ViewData["roommap"] = roomtemplate;
            List<List<int>> takenseats = _seatService.GetTakenSeats(tickets[0].show);
            foreach (Ticket ticket in tickets)
            {
                if (takenseats[ticket.SeatRow - 1].Contains(ticket.SeatNr))
                {
                    takenseats[ticket.SeatRow - 1].Remove(ticket.SeatNr);
                }
            }
            ViewData["takenseats"] = takenseats;
            return View(tickets);
        }

        [HttpPost]
        [Route("/tickets/confirmseatselection")]
        public IActionResult ConfirmSeatSelection(int orderid)
        {
            return RedirectToAction("Index", new
            {
                id = orderid
            });
        }

        [HttpGet]
        [Route("/tickets/reservation")]
        public IActionResult Reservation(
            [FromQuery] int showId,
            [FromQuery] int quantity,
            [FromQuery] bool abonnement,
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
            ViewBag.abonnement = abonnement;
            ViewBag.childDiscount = childDiscount;
            ViewBag.studentDiscount = studentDiscount;
            ViewBag.seniorDiscount = seniorDiscount;
            ViewBag.popcorn = popcorn;
            ViewBag.Arrangement = arrangement;

            var totalCost = _priceCalculatingService.ticketCost(quantity, showId, abonnement);
            var discount = _priceCalculatingService.Discount(childDiscount, studentDiscount, seniorDiscount);
            var premium = _priceCalculatingService.Premium(popcorn);
            var arrangementCost = _priceCalculatingService.ArrangementCost(arrangement);
            var arrangementTotal = arrangementCost * quantity;

            ViewBag.arrangementCost = arrangementCost;
            ViewBag.totalCost = totalCost;
            ViewBag.Discount = discount;
            ViewBag.Popcorn = premium;
            ViewBag.OrderCost = _priceCalculatingService.OrderCost(discount, premium, totalCost, arrangementTotal);
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
    }
}