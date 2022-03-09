using cinema.Data;
using cinema.Services;
using Microsoft.AspNetCore.Mvc;

namespace cinema.Controllers;

public class SeatsController : Controller
{
    private readonly CinemaContext _context;

    private readonly ISeatService _seatService;

    public SeatsController(CinemaContext context, ISeatService seatService)
    {
        _context = context;
        _seatService = seatService;
    }


    // GET
    [HttpGet]
    [Route("/seats")]
    public IActionResult Index([FromQuery] string show, [FromQuery] string quantity)
    {
        var seats = "";
        if (_context.Shows != null && _context.Shows.Any())
        {
            var showObject = _context.Shows.First(s => s.Id == int.Parse(show));
            seats = _seatService.GetSeats(showObject, int.Parse(quantity));
        }

        ViewBag.seats = seats;
        return View();
    }
}