using cinema.Data;
using cinema.Models;
using cinema.Repositories;
using Microsoft.EntityFrameworkCore;

namespace cinema.Services;

public class PriceCalculatingService : IPriceCalculatingService
{

    private readonly IShowRepository _showRepository;
    
    private readonly IMovieService _movieService;

    public PriceCalculatingService(IMovieService movieService, IShowRepository showRepository)
    {
        _movieService = movieService;
        _showRepository = showRepository;
    }

    public double pricePerTicket(int showId)
    {
        var show = _showRepository.FindShowByIdIncludeMovie(showId);
        var premium = 0.0;
        if (show.ThreeD)
        {
            premium = 3.5;
        }
        if (show.Movie.Length > 120)
        {
            return 9 + premium;
        }
        return 8.5 + premium;
    }

    public double ticketCost(int quantity, int showId)
    {
        return pricePerTicket(showId) * quantity;
    }

    public double Discount(int ChildDiscount, int StudentDiscount, int SeniorDiscount)
    {
        return (ChildDiscount + StudentDiscount + SeniorDiscount) * 1.5;
    }

    public double Premium(int Popcorn)
    {
        return Popcorn * 2.5;
    }

    public double ArrangementCost(Arrangements arrangement)
    {
        return arrangement switch
        {
            Arrangements.kinderfeestje => 5.0,
            Arrangements.vip => 4.0,
            Arrangements.normale => 0,
            _ => 0
        };
    }

    public double OrderCost(double Discount, double Premium, double SubTotalCost, double arrangementCost)
    {
        return SubTotalCost - Discount + Premium;
    }

    
}