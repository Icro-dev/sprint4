using cinema.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cinema.Controllers;

public class HomeController : Controller
{
    
    private readonly CinemaContext _context;

    public HomeController(CinemaContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    [Route("/")]
    public async Task<IActionResult> Index()
    {
        // @TODO extract to service
        DateTime movieWeek = DateTime.Now.AddDays(8);
        var shows  = await _context.Shows.Include(s => s.Movie).Where(s => s.StartTime <= movieWeek && s.StartTime >= DateTime.Now).OrderBy(s => s.StartTime).ToListAsync();
        return View(shows);
    }
}