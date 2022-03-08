using cinema.Data;
using cinema.Models;

namespace cinema.Services;

public class PriceCalculatingService : IPriceCalculatingService
{
    private readonly CinemaContext _context;

    public PriceCalculatingService(CinemaContext context)
    {
        _context = context;
    }

    public double pricePerTicket()
    {
        if (2 > _context.Movies.Find("When Harry Met Sally").Length)
        {
            return 8.5;
        }
        return 9;
    }

    public double totalPrice(int quantity)
    {
        return this.pricePerTicket() * quantity;
    }
}