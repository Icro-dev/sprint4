using cinema.Data;
using cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace cinema.Services;

public class PriceCalculatingService : IPriceCalculatingService
{
    private readonly CinemaContext _context;

    public PriceCalculatingService(CinemaContext context)
    {
        _context = context;
    }

    public double pricePerTicket(int showId)
    {
        //if (2 > _context.Movies.Find("When Harry Met Sally").Length)
        var show = _context.Shows.Include(s => s.Movie).First(s => s.Id == showId);
        if (show.Movie.Length > 120)
        {
            return 8.5;
        }
        return 9;
    }

    public double totalPrice(int quantity, int showId)
    {
        return pricePerTicket(showId) * quantity;
    }
}