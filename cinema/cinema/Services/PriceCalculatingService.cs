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
        //if (2 > _context.Movies.Find("When Harry Met Sally").Length)
        var show = _movieService.GetMovieFromShow(showId);
        if (show.Length > 120)
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