using Microsoft.AspNetCore.Mvc;

namespace cinema.Controllers;

public class SeatsController : Controller
{
    // GET
    [HttpGet]
    [Route("/seats")]
    public IActionResult Index()
    {
        return View();
    }
}