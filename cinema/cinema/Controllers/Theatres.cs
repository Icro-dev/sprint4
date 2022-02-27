using Microsoft.AspNetCore.Mvc;

namespace cinema.Controllers;

public class Theatres : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}