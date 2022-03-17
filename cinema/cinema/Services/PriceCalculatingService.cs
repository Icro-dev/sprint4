using cinema.Data;
using cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace cinema.Services;

public class PriceCalculatingService : IPriceCalculatingService
{
    private readonly CinemaContext _context;

    private readonly IMovieService _movieService;

    public PriceCalculatingService(CinemaContext context, IMovieService movieService)
    {
        _context = context;
        _movieService = movieService;
    }

    public double pricePerTicket(int showId)
    {
        var show = _context.Shows.Include(s => s.Movie).First(s => s.Id == showId);
        if (show.Movie.Length > 120)
        {
            return 8.5;
        }
        return 9;
    }

    public double ticketCost(int quantity, int showId)
    {
        return pricePerTicket(showId) * quantity;
    }

    public double Discount(int ChildDiscount, int StudentDiscount, int SeniorDiscount)
    {
        return (ChildDiscount + StudentDiscount + SeniorDiscount) * 2.5;
    }

    public double Premium(int Popcorn)
    {
        return Popcorn * 2.5;
    }

    public double OrderCost(double Discount, double Premium, double SubTotalCost)
    {
        return SubTotalCost - Discount + Premium;
    }

    
}