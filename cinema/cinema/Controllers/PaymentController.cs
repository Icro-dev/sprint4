using System.Text.Json;
using cinema.Data;
using cinema.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;

namespace cinema.Controllers;

public class PaymentController : Controller
{
    private readonly IConfiguration _config;
    private readonly IPaymentRepository _paymentRepository;
    
    private readonly string? _homeUrl;
    public PaymentController(IConfiguration config, IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository; 
        StripeConfiguration.ApiKey = Environment.GetEnvironmentVariable("StripeKey");
        _homeUrl = Environment.GetEnvironmentVariable("AppUrl");
    }

    [HttpPost("create-checkout-session")]
    public ActionResult CreateCheckoutSession([FromForm] int orderid)
    {
        var order = _paymentRepository.GetFirstOrder(orderid);
        var show = _paymentRepository.GetFirstShow(orderid);
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
            SuccessUrl = "https://"+_homeUrl+"/payment/PaymentSuccess?id="+orderid,
            CancelUrl = "https://"+_homeUrl+"/error",
        };

        var service = new SessionService();
        Console.WriteLine("*********************************************");
        Console.WriteLine( JsonSerializer.Serialize(options));
        Console.WriteLine("*********************************************");
        Session session = service.Create(options);

        Response.Headers.Add("Location", session.Url);
        return new StatusCodeResult(303);
    }

    [HttpGet]
    public RedirectToActionResult PaymentSucces(
        [FromQuery] int id)
    {
        _paymentRepository.GetUpdatePaymenttOrderSave(id);
        return RedirectToAction("Index", "Tickets", new {id = id});
    }
}