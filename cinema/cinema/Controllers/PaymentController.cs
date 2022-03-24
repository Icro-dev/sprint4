using System.Text.Json;
using cinema.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;

namespace cinema.Controllers;

public class PaymentController : Controller
{
    private readonly IConfiguration _config;
    private readonly CinemaContext _context;
    

    public PaymentController(IConfiguration config, CinemaContext context)
    {
        _config = config;
        _context = context;
        StripeConfiguration.ApiKey = _config["StripeKey"];

    }

    [HttpPost("create-checkout-session")]
    public ActionResult CreateCheckoutSession([FromForm] int orderid)
    {
        var order = _context.Orders.First(o => o.Id == orderid);
        var show = _context.Shows.Include(s => s.Movie).First(s => s.Id == order.ShowId);
        var cost = (int) order.Cost * 100;
        var options = new SessionCreateOptions
        {
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = cost,
                        Currency = "eur",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Bestelling :"+show.Movie.Name,
                        },

                    },
                    Quantity = 1,
                },
            },
            Mode = "payment",
            SuccessUrl = "https://localhost:7184/payment/PaymentSucces?id="+orderid,
            CancelUrl = "https://localhost:7184/error",
        };

        var service = new SessionService();
        Session session = service.Create(options);

        Response.Headers.Add("Location", session.Url);
        return new StatusCodeResult(303);
    }

    [HttpGet]
    public RedirectToActionResult PaymentSucces(
        [FromQuery] int id)
    {
        var order = _context.Orders.First(o => o.Id == id);
        order.IsPayed = true;
        _context.Orders.Update(order);
        _context.SaveChanges();
        return RedirectToAction("Index", "Tickets", new {id = id});
    }
}