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

        private readonly IRoomService _roomService;

        private readonly IConfiguration _config;


private readonly ISeatService _seatService;        public TicketsController(IPriceCalculatingService priceCalculatingService, ITicketService ticketService,
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
        [Route("/tickets/seatselection")]
        public IActionResult SeatSelection(string serializedTickets)
        {
            ViewData["serializedTickets"] = serializedTickets;
            List<Ticket> tickets = JsonConvert.DeserializeObject<List<Ticket>>(serializedTickets);
            Room room = _roomService.GetShowRoom(tickets[0].show);
            int[] roomtemplate = _roomService.GetRoomTemplate(room);
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
        public IActionResult SeatSelection(string serializedTickets, int seatrow, int seatnr)
        {
            List<Ticket> tickets = JsonConvert.DeserializeObject<List<Ticket>>(serializedTickets);
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
            ViewData["serializedTickets"] = JsonConvert.SerializeObject(tickets.ToList());
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
        public IActionResult ConfirmSeatSelection(string serializedTickets)
        {
            List<Ticket> tickets = JsonConvert.DeserializeObject<List<Ticket>>(serializedTickets);
            foreach (Ticket ticket in tickets)
                ticket.show = _context.Shows.Include(s => s.Movie).Where(s => s.Id == ticket.show.Id).First();
            _ticketService.PushTickets(tickets);
            return RedirectToAction("Index", new
            {
                serializedTickets = JsonConvert.SerializeObject(tickets.ToList())
            });
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

            return RedirectToAction("SeatSelection", new
            {
                id = order.Id
            });
        }
    }
}