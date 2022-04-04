using cinema.Data;
using cinema.Models;
using cinema.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace cinema.Controllers;

public class HomeController : Controller
{
    private readonly IHomeRepository _homeRepository;

    public HomeController(IHomeRepository homeRepository)
    {
        _homeRepository = homeRepository;
    }
    
    [HttpGet]
    [Route("/")]
    public IActionResult Index()
    {
        /*DateTime movieWeek = DateTime.Now.AddDays(8);*/
        var movies = _homeRepository.GetMoviesThatStartWithin8DaysSort();
        ViewBag.Movies = movies;
        return View();
    }

    public IActionResult Error(string error)
    {
        ViewBag.error = error;
        return View();
    }
}