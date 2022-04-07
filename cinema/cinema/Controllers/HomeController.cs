using cinema.Data;
using cinema.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
    public IActionResult Index()
    {
        DateTime movieWeek = DateTime.Now.AddDays(8);
        var movies  =  _context.Shows.Include(s => s.Movie).Where(s => s.StartTime <= movieWeek && s.StartTime >= DateTime.Now).OrderBy(s => s.StartTime).Select(s => s.Movie).Distinct();
        ViewBag.Movies = movies;
        return View();
    }

    public IActionResult Error(string error)
    {
        ViewBag.error = error;
        return View();
    }
}