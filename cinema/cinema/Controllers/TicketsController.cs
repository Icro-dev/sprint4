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

        private readonly IRoomService _roomService;

        private readonly ISeatService _seatService;


        public TicketsController(IPriceCalculatingService priceCalculatingService, ITicketService ticketService, IPaymentAdapter paymentAdapter, IMovieService movieService, IRoomService roomService, ISeatService seatService, CinemaContext context)
        {
            _ticketService = ticketService;
            _paymentAdapter = paymentAdapter;
            _movieService = movieService;
            _context = context;
            _priceCalculatingService = priceCalculatingService;
            _roomService = roomService;
            _seatService = seatService;
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
        [Route("/tickets/quantity")]
        public IActionResult Quantity([FromQuery] int showId)
        {
            Movie movie = _movieService.GetMovieFromShow(showId);
            ViewBag.Movie = movie;
            ViewBag.ShowId = showId;
            return View();
        }

        [HttpGet]
        [Route("/tickets/create")]
        public IActionResult Create([FromQuery] int showId, [FromQuery] int quantity)
        {
            ViewBag.show = showId;
            ViewBag.price = _priceCalculatingService.pricePerTicket(showId);
            ViewBag.totalPrice = _priceCalculatingService.ticketCost(showId, quantity);
            ViewBag.quantity = quantity;
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
            [FromQuery] int popcorn)
        {
            
            ViewBag.quantity = quantity;
            ViewBag.showId = showId;
            ViewBag.childDiscount = childDiscount;
            ViewBag.studentDiscount = studentDiscount;
            ViewBag.seniorDiscount = seniorDiscount;
            ViewBag.popcorn = popcorn;

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
            [FromForm] int popcorn)
        {
           var tickets =  _ticketService.CreateTickets(
                showId,
                quantity,
                childDiscount,
                seniorDiscount,
                studentDiscount,
                popcorn);

           var myTicketsId = new List<int>();
           foreach (var ticket in tickets)
           {
               myTicketsId.Add(ticket.Id);
           }
           TempData["myTickets"] = myTicketsId;

            return RedirectToAction("SeatSelection", new
            {
                serializedTickets = JsonConvert.SerializeObject(tickets.ToList()) 
            });
        }
    }
}