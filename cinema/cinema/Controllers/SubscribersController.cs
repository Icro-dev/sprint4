using cinema.Data;
using cinema.Models;
using Microsoft.AspNetCore.Mvc;

namespace cinema.Controllers;

public class SubscribersController : Controller
{
    private readonly CinemaContext _context;

    public SubscribersController(CinemaContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    [Route("/subscriber")]
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    [Route("/subscriber/create")]
    public IActionResult Create(
        [FromForm] string email)
    {
        Subscriber subscriber = new Subscriber();
        subscriber.Email = email;
        _context.Add(subscriber);
        _context.SaveChanges();
        return View(subscriber);
    }
}