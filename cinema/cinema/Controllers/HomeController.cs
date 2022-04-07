using cinema.Data;
using cinema.Models;
using cinema.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace cinema.Controllers;

public class HomeController : Controller
{
    
    private readonly IHomeRepository homeRepository;

    public HomeController(IHomeRepository homeRepository)
    {
        this.homeRepository = homeRepository;
    }

    [HttpGet]
    [Route("/")]
    public IActionResult Index()
    {
        DateTime movieWeek = DateTime.Now.AddDays(8);
        var movies  =  homeRepository.GetMoviesThatStartWithin8DaysSort();
        ViewBag.Movies = movies;
        return View();
    }

    public IActionResult Error(string error)
    {
        ViewBag.error = error;
        return View();
    }
}