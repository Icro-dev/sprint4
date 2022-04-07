using cinema.Data;
using cinema.Models;
using cinema.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace cinema.Controllers;

public class SubscribersController : Controller
{
    private readonly ISubscriberRepository _subscriberRepository;

    public SubscribersController(ISubscriberRepository subscriberRepository)
    {
        _subscriberRepository = subscriberRepository;
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
        
        _subscriberRepository.addSubscriber(subscriber);
        _subscriberRepository.saveSubscriber();
        return View(subscriber);
    }
}